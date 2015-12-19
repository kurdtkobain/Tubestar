using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for AddVideoDialog.xaml
    /// </summary>
    public partial class AddVideoDialog : ChildWindow
    {
        public Video Video { get; private set; }

        public AddVideoDialog()
        {
            InitializeComponent();
            Translate();
            FocusedElement = txtName;

            var data = GetData();
            cmbCategory.ItemsSource = data;
            cmbCategory.DisplayMemberPath = "Value";
            cmbCategory.SelectedValuePath = "Key";

            if (Settings.LastCategory.HasValue)
            {
                cmbCategory.SelectedValue = Settings.LastCategory.Value;
            }
        }

        private void Translate()
        {
            Caption = EnglishStrings.AddVideo.Translate();
            lblName.Text = EnglishStrings.Name.Translate() + ":";
            lblCategory.Text = EnglishStrings.Category.Translate() + ":";
            lblHourSelect.Text = EnglishStrings.Hours.Translate() + ":";
            lblCost.Text = EnglishStrings.Cost.Translate() + ":";
            btnOk.Content = EnglishStrings.Next.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
            lblHours.Text = "2 " + EnglishStrings.Hours.Translate().ToLower();
        }

        private Dictionary<VideoCategory, string> GetData()
        {
            Dictionary<VideoCategory, string> data = new Dictionary<VideoCategory, string>();
            foreach (VideoCategory category in Enum.GetValues(typeof(VideoCategory)))
            {
                data[category] = category.GetString();
            }

            List<KeyValuePair<VideoCategory, string>> sortTemp = data.ToList();
            sortTemp.Sort((l, r) => l.Value.CompareTo(r.Value));

            return sortTemp.ToDictionary((s) => s.Key, (s) => s.Value);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingName.Translate(), MessagePicture.Puzzle);
                return;
            }

            if (cmbCategory.SelectedValue == null)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingCategory.Translate(), MessagePicture.Puzzle);
                return;
            }

            if (Player.Current.Money - (sldrMoney.Value * 100) < 0)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.LowCashHeader.Translate(), EnglishStrings.LowCashMessage.Translate(), MessagePicture.Money);
                return;
            }

            Video = new Video();
            Video.Name = txtName.Text;
            Video.Category = (VideoCategory)cmbCategory.SelectedValue;
            Video.ExtraShootingHours = (int)sldrHours.Value - ShootVideo.MinimumShootTime;
            Video.Cost = (int)sldrMoney.Value * 100;

            Settings.LastCategory = Video.Category;
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

        private void sldrMoney_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblMoney != null)
                lblMoney.Text = String.Format("${0}", (int)sldrMoney.Value * 100);
        }
    }
}