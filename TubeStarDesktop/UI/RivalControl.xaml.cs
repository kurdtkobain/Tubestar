using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for RivalControl.xaml
    /// </summary>
    public partial class RivalControl : UserControl
    {
        private Channel _currentChannel;
        private int _tier = 1;

        private const int RivalsPerTier = 3;

        public RivalControl()
        {
            InitializeComponent();
            PopulateRivals();
            Translate();
        }

        private void PopulateRivals()
        {
            foreach (var rival in Rivals.Current.All)
            {
                if (rival.Channel == null)
                {
                    rival.CreateChannel();
                }
            }
        }

        private void Translate()
        {
        }

        public void Update()
        {
            channelPanel.Children.Clear();

            List<Channel> channels = new List<Channel>();
            foreach (var rival in Rivals.Current.All)
            {
                if (rival.Channel != null)
                {
                    channels.Add(rival.Channel);
                }
            }
            foreach (var channel in Player.Current.Channels)
            {
                if (channel != Channel.UnreleasedVideos)
                    channels.Add(channel);
            }

            channels.Sort((l, r) =>
                {
                    if (l == null || r == null)
                        return -1;
                    return l.Subscribers.CompareTo(r.Subscribers);
                });

            if (channels.Count > 0 && !channels.Last().IsRivalChannel)
            {
                TrophyManager.UnlockTrophy(Trophy.King);
            }

            int taken = 0;
            int skip = (_tier - 1) * RivalsPerTier;
            foreach (var channel in channels)
            {
                if (skip == 0)
                {
                    if (_currentChannel == null)
                        _currentChannel = channel;

                    RenderChannel(channel);
                    if (channel.IsRivalChannel)
                    {
                        taken++;
                        if (taken == RivalsPerTier)
                        {
                            if (_tier != 5)
                                break;
                        }
                    }
                }
                else
                {
                    if (channel.IsRivalChannel)
                        skip--;
                }
            }

            ShowChannel(_currentChannel);
        }

        private void RenderChannel(Channel channel)
        {
            var channelBlock = new ChannelBlock(channel, channel == _currentChannel);
            channelBlock.HideEdit();
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
            channelPanel.Children.Insert(0, channelBlock);
        }

        private void ShowChannel(Channel channel)
        {
            if (channel == null)
                return;

            channelSummaryTextBlock.Text = String.Format("{0}: {1}", EnglishStrings.Subscribers.Translate(), channel.Subscribers.ToNumberString());

            videoPanel.Children.Clear();
            videoPanelGrid.Children.Clear();
            foreach (var video in channel.Videos)
            {
                if (Settings.ListView)
                {
                    listViewer.Visibility = Visibility.Visible;
                    gridViewer.Visibility = Visibility.Collapsed;

                    var videoBlock = new VideoBlock(channel, video);
                    videoPanel.Children.Insert(0, videoBlock);
                }
                else
                {
                    listViewer.Visibility = Visibility.Collapsed;
                    gridViewer.Visibility = Visibility.Visible;

                    var videoBlock = new VideoBlockGrid(channel, video);
                    videoPanelGrid.Children.Insert(0, videoBlock);
                }
            }
        }

        internal void NewDay()
        {
            if (RandomHelpers.Chance(75))
            {
                Rivals.Current.All[RandomHelpers.RandomInt(Rivals.Current.All.Count)].AddNewVideo();
            }
            if (RandomHelpers.Chance(75))
            {
                Rivals.Current.All[RandomHelpers.RandomInt(Rivals.Current.All.Count)].AddNewVideo();
            }
        }

        private void UnCheckAll()
        {
            btnTier1.IsChecked = btnTier2.IsChecked = btnTier3.IsChecked = btnTier4.IsChecked = btnTier5.IsChecked = false;
            _currentChannel = null;
        }

        private void btnTier1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UnCheckAll();
            btnTier1.IsChecked = true;
            _tier = 1;
            Update();
        }

        private void btnTier2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UnCheckAll();
            btnTier2.IsChecked = true;
            _tier = 2;
            Update();
        }

        private void btnTier3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UnCheckAll();
            btnTier3.IsChecked = true;
            _tier = 3;
            Update();
        }

        private void btnTier4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UnCheckAll();
            btnTier4.IsChecked = true;
            _tier = 4;
            Update();
        }

        private void btnTier5_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UnCheckAll();
            btnTier5.IsChecked = true;
            _tier = 5;
            Update();
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
    }
}