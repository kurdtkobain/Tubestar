using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for VideoBlock.xaml
    /// </summary>
    public partial class VideoBlock : UserControl
    {
        public event EventHandler UploadClick;

        public event EventHandler LawyerClick;

        public event EventHandler DeleteClick;

        private Channel _channel;

        private Video _video;

        public Video Video
        {
            get { return _video; }
            private set
            {
                _video = value;
                DataContext = new VideoDataContext(_video);
                if (_video != null)
                {
                    if (_channel == null)
                        throw new NotSupportedException("Channel cannot be null");

                    bool isUnreleasedChannel = (_channel == Channel.UnreleasedVideos);

                    txtName.Text = _video.Name;
                    txtViews.Text = String.Format("{0} {1}", GetNumberOfViews(), EnglishStrings.Views.Translate());
                    txtLikes.Text = _video.Likes.ToNumberString();
                    txtDislikes.Text = _video.Dislikes.ToNumberString();

                    panelStats.Visibility = txtViews.Visibility = isUnreleasedChannel ? Visibility.Collapsed : Visibility.Visible;

                    txtQuality.Text = GetQualityText(isUnreleasedChannel);

                    btnComments.Visibility = isUnreleasedChannel ? Visibility.Collapsed : Visibility.Visible;
                    btnUpload.Visibility = isUnreleasedChannel ? Visibility.Visible : Visibility.Collapsed;

                    btnDelete.Visibility = _channel.IsRivalChannel ? Visibility.Collapsed : Visibility.Visible;

                    SuspendedAdorner.Visibility = _video.IsSuspended ? Visibility.Visible : Visibility.Collapsed;
                    txtSuspendedSub.Visibility = Visibility.Collapsed;
                    if (_video.IsSuspended && StoreItems.Current.Lawyer.Purchased)
                    {
                        txtSuspendedSub.Visibility = Visibility.Visible;
                    }

                    if (Video.ImageBytes != null)
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.StreamSource = StreamHelpers.BytesToStream(Video.ImageBytes);
                        image.EndInit();
                        imgVideo.Source = image;
                    }
                    else
                    {
                        Video.OnImageFetched += () =>
                        {
                            BitmapImage image = new BitmapImage();
                            image.BeginInit();
                            image.StreamSource = StreamHelpers.BytesToStream(Video.ImageBytes);
                            image.EndInit();
                            imgVideo.Source = image;
                        };
                    }
                }
            }
        }

        private string GetNumberOfViews()
        {
            if (_video.Views > 301 && Video.Iterations == 1)
                return "301+";
            return _video.Views.ToNumberString();
        }

        private void SuspendedAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (LawyerClick != null && StoreItems.Current.Lawyer.Purchased && Video.IsSuspended)
                LawyerClick(this, EventArgs.Empty);
        }

        private int GetCount(Video video)
        {
            int count = video.Comments.Count;
            video.Comments.ForEach(c => count += c.Likes);
            return count;
        }

        private string GetQualityText(bool isUnreleasedChannel)
        {
            bool canView = !isUnreleasedChannel || Player.Current.CanViewQualityBeforeUpload;
            qualityGrid.Visibility = canView ? Visibility.Visible : Visibility.Collapsed;
            return String.Format("{0}", canView ? String.Format("{0}", _video.GenerateQuality()) : "-");
        }

        public VideoBlock(Channel channel, Video video)
        {
            InitializeComponent();
            _channel = channel;
            Video = video;
            Translate();
        }

        private void Translate()
        {
            txtSuspended.Text = EnglishStrings.Suspended.Translate().ToUpper();
            txtSuspendedSub.Text = EnglishStrings.SuspendedHireLawyer.Translate();
            btnUpload.Content = EnglishStrings.UploadVideo.Translate();
            btnComments.Content = String.Format("{0} ({1})", EnglishStrings.Comments.Translate(), _video.GetCommentsCount().ToNumberString());
        }

        private void imgVideo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewVideoDialog dlg = new ViewVideoDialog(Video);
            dlg.ShowDialog();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (Video.IsSuspended) return;

            if (_channel == Channel.UnreleasedVideos)
            {
                if (UploadClick != null)
                    UploadClick(this, EventArgs.Empty);
            }
        }

        private void btnComments_Click(object sender, RoutedEventArgs e)
        {
            if (Video.IsSuspended) return;

            ViewVideoDialog dlg = new ViewVideoDialog(Video);
            dlg.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.ShowDialog(EnglishStrings.Delete.Translate(), EnglishStrings.DeleteVideo.Translate(), MessagePicture.Question, (result) =>
                {
                    if (result == true)
                    {
                        if (DeleteClick != null)
                            DeleteClick(this, EventArgs.Empty);
                    }
                });
        }
    }
}