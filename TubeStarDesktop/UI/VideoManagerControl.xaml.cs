using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for VideoManagerControl.xaml
    /// </summary>
    public partial class VideoManagerControl : UserControl
    {
        private Channel _currentChannel;

        public VideoManagerControl()
        {
            InitializeComponent();
            Translate();
            _currentChannel = Channel.UnreleasedVideos;
        }

        private void Translate()
        {
            txtAddChannel.Text = EnglishStrings.AddChannel.Translate();
            btnStats.ToolTip = EnglishStrings.Stats.Translate();
        }

        private void AddChannel_Click(object sender, RoutedEventArgs e)
        {
            AddChannelDialog channelDialog = new AddChannelDialog();
            channelDialog.ShowDialog(() =>
                {
                    if (channelDialog.Channel != null)
                    {
                        Player.Current.Channels.Add(channelDialog.Channel);
                        Update();
                    }
                });
        }

        public void Update()
        {
            //Update unreleased videos channel...
            Channel.UnreleasedVideos.Videos.Clear();
            foreach (var video in Player.Current.Videos)
            {
                if (!video.HasBeenReleased && video.HasBeenRendered)
                {
                    Channel.UnreleasedVideos.Videos.Add(video);
                }
            }

            channelPanel.Children.Clear();
            foreach (var channel in Player.Current.Channels.Where(c => !c.IsSuspended))
            {
                RenderChannel(channel);
            }

            foreach (var channel in Player.Current.Channels.Where(c => c.IsSuspended))
            {
                RenderChannel(channel);
            }

            ShowChannel(_currentChannel);
        }

        private void RenderChannel(Channel channel)
        {
            var channelBlock = new ChannelBlock(channel, channel == _currentChannel);
            channelBlock.ChannelClick += (s, e) =>
            {
                foreach (ChannelBlock block in channelPanel.Children)
                {
                    block.Selected = false;
                }

                channelBlock.Selected = true;
                _currentChannel = channelBlock.Channel;
                ShowChannel(channelBlock.Channel);
            };
            channelBlock.EditClick += (s, e) =>
            {
                AddChannelDialog channelDialog = new AddChannelDialog(channelBlock.Channel);
                channelDialog.ShowDialog(() =>
                {
                    if (channelDialog.DialogResult == true)
                    {
                        channelBlock.Channel.Name = channelDialog.Channel.Name;
                        channelBlock.Channel.Advertising = channelDialog.Channel.Advertising;
                        Update();
                    }
                });
            };
            channelPanel.Children.Add(channelBlock);
        }

        private void ShowChannel(Channel channel)
        {
            channelSummaryTextBlock.Text = (channel == Channel.UnreleasedVideos) ?
                EnglishStrings.NotApplicable.Translate() :
                channel.IsSuspended ? EnglishStrings.Suspended.Translate().ToUpper() : String.Format("{0}: {1}  {2}: {3}", EnglishStrings.Subscribers.Translate(), channel.Subscribers.ToNumberString(), EnglishStrings.ChannelIncome.Translate(), channel.Income.ToCurrencyString());
            btnStats.Visibility = (channel == Channel.UnreleasedVideos) ? Visibility.Collapsed : Visibility.Visible;

            if (channel.Videos.Count >= 100)
                TrophyManager.UnlockTrophy(Trophy.OCD);

            videoPanel.Children.Clear();
            videoPanelGrid.Children.Clear();
            foreach (var video in channel.Videos)
            {
                if (Settings.ListView)
                {
                    listViewer.Visibility = Visibility.Visible;
                    gridViewer.Visibility = Visibility.Collapsed;

                    var videoBlock = new VideoBlock(channel, video);
                    videoBlock.UploadClick += (s, e) =>
                    {
                        UploadVideo(videoBlock.Video);
                    };
                    videoBlock.DeleteClick += (s, e) =>
                    {
                        DeleteVideo(videoBlock.Video);
                    };
                    videoBlock.LawyerClick += (s, e) =>
                    {
                        HandleLawyerClick(videoBlock.Video);
                    };
                    videoPanel.Children.Insert(0, videoBlock);

                    binGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    listViewer.Visibility = Visibility.Collapsed;
                    gridViewer.Visibility = Visibility.Visible;

                    var videoBlock = new VideoBlockGrid(channel, video);
                    videoBlock.UploadClick += (s, e) =>
                    {
                        UploadVideo(videoBlock.Video);
                    };
                    videoBlock.LawyerClick += (s, e) =>
                    {
                        HandleLawyerClick(videoBlock.Video);
                    };
                    videoPanelGrid.Children.Insert(0, videoBlock);

                    binGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void HandleLawyerClick(Video video)
        {
            if (_currentChannel.IsSuspended)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.LawyerStoreItem.Translate(), String.Format(EnglishStrings.RemoveChannelSuspension.Translate(), StoreItems.Current.Lawyer.AdditionalCost.ToCurrencyString()), MessagePicture.Legal, (result) =>
                {
                    if (result == true)
                    {
                        _currentChannel.IsSuspended = false;
                        foreach (var v in _currentChannel.Videos)
                        {
                            v.IsSuspended = false;
                        }
                        Player.Current.Money -= StoreItems.Current.Lawyer.AdditionalCost;
                        Update();
                    }
                });
            }
            else if (video.IsSuspended)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.LawyerStoreItem.Translate(), String.Format(EnglishStrings.RemoveVideoSuspension.Translate(), StoreItems.Current.Lawyer.AdditionalCost.ToCurrencyString()), MessagePicture.Legal, (result) =>
                {
                    if (result == true)
                    {
                        video.IsSuspended = false;
                        Player.Current.Money -= StoreItems.Current.Lawyer.AdditionalCost;
                        Update();
                    }
                });
            }
        }

        private void UploadVideo(Video video)
        {
            if (Player.Current.Channels.Count == 1)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.WhereTo.Translate(), EnglishStrings.ChannelNeeded.Translate(), MessagePicture.Puzzle);
            }
            else
            {
                UploadVideoDialog uploadDialog = new UploadVideoDialog(video);
                uploadDialog.ShowDialog(() =>
                    {
                        if (uploadDialog.Channel != null)
                        {
                            //Generate comments
                            CommentGenerator.GenerateRealComments(uploadDialog.Video.YouTubeVideoId);

                            //Subscriber views
                            VideoViewer.SubscriberView(uploadDialog.Channel, uploadDialog.Video);

                            video.GenerateQuality();

                            //Consultant fee
                            if (StoreItems.Current.Consultant.Purchased)
                            {
                                video.OnceOffCost += StoreItems.Current.Consultant.AdditionalCost;
                            }

                            //Special attribute - CopyCat
                            if (uploadDialog.Video.Attributes.Contains(VideoAttributes.CopyCat))
                            {
                                VideoViewer.FreeViews(uploadDialog.Video, VideoAttributes.CopyCat.FreeViews);
                            }

                            //Special attribute - FanBoyBait
                            if (video.Quality >= 75 && uploadDialog.Video.Attributes.Contains(VideoAttributes.FanBoyBait))
                            {
                                uploadDialog.Channel.MinimumSubsribers = uploadDialog.Channel.Subscribers;
                                TrophyManager.UnlockTrophy(Trophy.HookLineAndSinker);
                            }

                            //Trophy - PoopStar
                            if (video.Quality <= 1)
                            {
                                TrophyManager.UnlockTrophy(Trophy.PoopStar);
                            }

                            uploadDialog.Channel.Videos.Add(uploadDialog.Video);
                            Player.Current.Videos.Remove(uploadDialog.Video);
                            Update();
                        }
                    });
            }
        }

        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            ChannelStatsDialog dialog = new ChannelStatsDialog(_currentChannel);
            dialog.ShowDialog();
        }

        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ListView = false;
            Settings.ListView = false;
            Update();
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ListView = true;
            Settings.ListView = true;
            Update();
        }

        private void binGrid_DragEnter(object sender, DragEventArgs e)
        {
            binGrid.Background = Brushes.Silver;
        }

        private void binGrid_DragLeave(object sender, DragEventArgs e)
        {
            binGrid.Background = Brushes.Transparent;
        }

        private void binGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("TubeStar.Video"))
            {
                Video video = e.Data.GetData("TubeStar.Video") as Video;
                DeleteVideo(video);
            }
        }

        private void DeleteVideo(Video video)
        {
            if (video.Category == VideoCategory.Cats)
                TrophyManager.UnlockTrophy(Trophy.CatInBin);

            if (_currentChannel == Channel.UnreleasedVideos)
            {
                Player.Current.Videos.Remove(video);
            }
            else
            {
                _currentChannel.Videos.Remove(video);
            }

            binGrid.Background = Brushes.Transparent;
            Update();
        }
    }
}