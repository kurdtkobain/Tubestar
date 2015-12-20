using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for EditVideoDialog.xaml
    /// </summary>
    public partial class EditVideoDialog : ChildWindow
    {
        public Video Video { get; private set; }
        public int Episodes { get; private set; }

        public EditVideoDialog(List<Video> videos)
        {
            InitializeComponent();
            Translate();
            FocusedElement = cmbVideo;

            cmbVideo.ItemsSource = GetData(videos);
            cmbVideo.DisplayMemberPath = "Value";
            cmbVideo.SelectedValuePath = "Key";
            cmbVideo.SelectedIndex=0;
            if (videos.Count == 1)
            {
                cmbVideo.IsEnabled = false;
            }
        }

        private void Translate()
        {
            Caption = EnglishStrings.EditVideo.Translate();
            lblVideo.Text = EnglishStrings.Video.Translate() + ":";
            lblHoursSelect.Text = EnglishStrings.Hours.Translate() + ":";
            btnOk.Content = EnglishStrings.Ok.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
            lblHours.Text = "4 " + EnglishStrings.Hours.Translate().ToLower();
        }

        private Dictionary<Video, string> GetData(List<Video> videos)
        {
            Dictionary<Video, string> data = new Dictionary<Video, string>();
            foreach (var video in videos)
            {
                data[video] = video.Name;
            }
            return data;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbVideo.SelectedValue == null)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingVideo.Translate(), MessagePicture.Puzzle);
                return;
            }

            Video = (Video)cmbVideo.SelectedValue;
            Video.ExtraEditingHours = (int)sldrHours.Value - EditVideo.MinimumEditTime;
            this.Episodes = (int)sldrEpisodes.Value;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sldrHours_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblHours != null)
                lblHours.Text = String.Format("{0} {1}", (int)sldrHours.Value, EnglishStrings.Hours.Translate().ToLower());
        }

        private void cmbVideo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbVideo.SelectedValue != null)
            {
                var sel = (Video)cmbVideo.SelectedValue;
                if (sel.Category != VideoCategory.Gaming)
                {
                    sldrEpisodes.Maximum = 1;
                    lblEpisodes.Visibility = lblEpisodesSelect.Visibility = sldrEpisodes.Visibility = Visibility.Collapsed;
                }
                else
                {
                    int tmpMax =(ShootVideo.MinimumShootTime + sel.ExtraShootingHours) * 60 / 15;
                    sldrEpisodes.Maximum = tmpMax;
                    lblEpisodes.Visibility = lblEpisodesSelect.Visibility = sldrEpisodes.Visibility = Visibility.Visible;

                }
            }
        }

        private void sldrEpisodes_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblEpisodes != null)
                lblEpisodes.Text = String.Format("{0}", (int)sldrEpisodes.Value);
        }
    }
}