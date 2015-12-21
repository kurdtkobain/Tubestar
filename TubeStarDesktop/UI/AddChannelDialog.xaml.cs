using System;
using System.Collections.Generic;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for AddChannelDialog.xaml
    /// </summary>
    public partial class AddChannelDialog : ChildWindow
    {
        public Channel Channel { get; private set; }

        public AddChannelDialog()
        {
            InitializeComponent();
            Translate();
            FocusedElement = txtName;

            cmbAdvertising.ItemsSource = GetData();
            cmbAdvertising.DisplayMemberPath = "Value";
            cmbAdvertising.SelectedValuePath = "Key";

            cmbAdvertising.SelectedValue = AdvertisingStrategy.Normal;
        }

        private void Translate()
        {
            Caption = EnglishStrings.AddChannel.Translate();
            lblName.Text = EnglishStrings.Name.Translate() + ":";
            lblStrategy.Text = EnglishStrings.AdvertisingStrategy.Translate() + ":";
            btnOk.Content = EnglishStrings.Ok.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
        }

        public AddChannelDialog(Channel channel)
            : this()
        {
            Caption = EnglishStrings.EditChannel.Translate();
            Channel = channel;
            txtName.Text = channel.Name;
            cmbAdvertising.SelectedValue = channel.Advertising;
        }

        private Dictionary<AdvertisingStrategy, string> GetData()
        {
            Dictionary<AdvertisingStrategy, string> data = new Dictionary<AdvertisingStrategy, string>();
            data[AdvertisingStrategy.Low] = GetEnumString(AdvertisingStrategy.Low);
            data[AdvertisingStrategy.Normal] = GetEnumString(AdvertisingStrategy.Normal);
            data[AdvertisingStrategy.High] = GetEnumString(AdvertisingStrategy.High);
            data[AdvertisingStrategy.Aggressive] = GetEnumString(AdvertisingStrategy.Aggressive);
            return data;
        }

        private string GetEnumString(AdvertisingStrategy strategy)
        {
            var income = strategy.GetAttribute<AdvertistingIncomeAttribute>().IncomePerView;
            return String.Format("{0} ({1} {2}) {3}", strategy.GetString(),
                Player.Current.ChallengeMode ? (income / 2).ToCurrencyString() : income.ToCurrencyString(),
                EnglishStrings.PerView.Translate(),
                Player.Current.UltraMode ? "(CTR - 10%)" : (Player.Current.ChallengeMode ? "(CTR - 50%)" : ""));
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingName.Translate(), MessagePicture.Puzzle);
                return;
            }

            if (cmbAdvertising.SelectedValue == null)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingStrategy.Translate(), MessagePicture.Puzzle);
                return;
            }

            Channel = new Channel()
            {
                Name = txtName.Text,
                Advertising = (AdvertisingStrategy)cmbAdvertising.SelectedValue,
            };
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}