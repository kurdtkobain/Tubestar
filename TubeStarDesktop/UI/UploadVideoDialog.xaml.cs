using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for UploadVideoDialog.xaml
    /// </summary>
    public partial class UploadVideoDialog : ChildWindow
    {
        private List<string> _videoImageIds;
        private int _imageIndex;

        public Channel Channel { get; private set; }
        public Video Video { get; private set; }

        public UploadVideoDialog(Video video)
        {
            InitializeComponent();
            Translate();

            Video = video;

            _videoImageIds = new List<string>();
            _videoImageIds.Add(Video.YouTubeVideoId);
            _imageIndex = 0;
            DoPopulateImages();

            var data = GetData();
            cmbChannel.ItemsSource = data;
            cmbChannel.DisplayMemberPath = "Value";
            cmbChannel.SelectedValuePath = "Key";

            if (data.Count == 1)
            {
                cmbChannel.SelectedValue = data.First().Key;
                cmbChannel.IsEnabled = false;
            }
            else if (Settings.LastChannel != null && !Settings.LastChannel.IsSuspended)
            {
                cmbChannel.SelectedValue = Settings.LastChannel;
            }

            if (Video.ImageBytes != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = StreamHelpers.BytesToStream(Video.ImageBytes);
                image.EndInit();
                imgVideo.Source = image;
            }

            Video.OnImageFetched += () =>
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = StreamHelpers.BytesToStream(Video.ImageBytes);
                image.EndInit();
                imgVideo.Source = image;
            };
        }

        private void DoPopulateImages()
        {
            int randomImageCount = 50;
            var uri = YouTubeAPI.GetRandomImages(Video.Name, randomImageCount);
            WebClientHelpers.Download<YouTubeSearchResponse>(uri, (response) =>
            {
                if (response != null && response.Entries != null && response.Entries.Count > 0)
                {
                    _videoImageIds.AddRange(response.Entries.ConvertAll(e => e.Id.VideoId));
                    UpdateButtons();
                }
                else
                {
                    //Limit to category
                    uri = YouTubeAPI.GetRandomImages(Video.Category.GetString(), randomImageCount);
                    WebClientHelpers.Download<YouTubeSearchResponse>(uri, (r) =>
                    {
                        if (r != null && r.Entries != null && r.Entries.Count > 0)
                        {
                            _videoImageIds.AddRange(response.Entries.ConvertAll(e => e.Id.VideoId));
                            UpdateButtons();
                        }
                    }, null);
                }
            }, null);
        }

        private void UpdateButtons()
        {
            Prev.Visibility = _imageIndex > 0 ? Visibility.Visible : Visibility.Hidden;
            Next.Visibility = _imageIndex < _videoImageIds.Count - 1 ? Visibility.Visible : Visibility.Hidden;
        }

        private void Translate()
        {
            Caption = EnglishStrings.UploadVideo.Translate();
            txtRandomImage.Text = EnglishStrings.FetchNewImage.Translate();
            lblBuyViews.Text = EnglishStrings.BuyViews.Translate() + ":";
            lblChannel.Text = EnglishStrings.Channel.Translate() + ":";
            txtNone.Text = EnglishStrings.None.Translate();
            txt100.Text = String.Format("1000 {0} ({1}: $100)", EnglishStrings.Views.Translate(), EnglishStrings.Cost.Translate());
            txt200.Text = String.Format("2000 {0} ({1}: $200)", EnglishStrings.Views.Translate(), EnglishStrings.Cost.Translate());
            txt2c.Text = String.Format("1000 {0} ({1}: $0.02 {2})", EnglishStrings.Views.Translate(), EnglishStrings.Cost.Translate(), EnglishStrings.PerView.Translate());
            txt4c.Text = String.Format("2000 {0} ({1}: $0.04 {2})", EnglishStrings.Views.Translate(), EnglishStrings.Cost.Translate(), EnglishStrings.PerView.Translate());
            btnOk.Content = EnglishStrings.Ok.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
        }

        private Dictionary<Channel, string> GetData()
        {
            Dictionary<Channel, string> data = new Dictionary<Channel, string>();
            foreach (var channel in Player.Current.Channels)
            {
                if (channel != Channel.UnreleasedVideos && !channel.IsSuspended)
                    data[channel] = channel.Name;
            }
            return data;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbChannel.SelectedValue == null)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingChannel.Translate(), MessagePicture.Puzzle);
                return;
            }

            if (rb100.IsChecked == true)
            {
                if (Player.Current.Money - 100 < 0)
                {
                    CustomMessageBox.ShowDialog(EnglishStrings.LowCashHeader.Translate(), EnglishStrings.LowCashMessage.Translate(), MessagePicture.Money);
                    return;
                }
                Video.OnceOffCost += 100;
                VideoViewer.BuyViews(Video, 1000);
            }
            else if (rb200.IsChecked == true)
            {
                if (Player.Current.Money - 200 < 0)
                {
                    CustomMessageBox.ShowDialog(EnglishStrings.LowCashHeader.Translate(), EnglishStrings.LowCashMessage.Translate(), MessagePicture.Money);
                    return;
                }
                Video.OnceOffCost += 200;
                VideoViewer.BuyViews(Video, 2000);
            }
            else if (rb2c.IsChecked == true)
            {
                VideoViewer.BuyViews(Video, 1000);
                Video.CostPerView = 0.02;
            }
            else if (rb4c.IsChecked == true)
            {
                VideoViewer.BuyViews(Video, 2000);
                Video.CostPerView = 0.04;
            }

            Channel = Settings.LastChannel = (Channel)cmbChannel.SelectedValue;
            Video.HasBeenReleased = true;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void YT_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.youtube.com");
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                btn.Opacity = 1;
            }
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                btn.Opacity = 0.5;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex++;
            Video.SetImageFromId(_videoImageIds[_imageIndex]);
            UpdateButtons();
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex--;
            Video.SetImageFromId(_videoImageIds[_imageIndex]);
            UpdateButtons();
        }

        private void FetchImage_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex = RandomHelpers.RandomInt(_videoImageIds.Count);
            Video.SetImageFromId(_videoImageIds[_imageIndex]);
            UpdateButtons();
        }
    }
}