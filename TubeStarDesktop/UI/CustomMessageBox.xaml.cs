using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    public enum MessagePicture
    {
        Axe,
        Error,
        Happy,
        Legal,
        Money,
        Puzzle,
        Question,
        Robot,
        Sad,
        Score,
        Static,
        Study,
        Work
    }

    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : ChildWindow
    {
        public CustomMessageBox(string title, string text, MessagePicture picture)
        {
            InitializeComponent();
            Translate();

            Caption = title;
            messageBoxTextBlock.Text = text;
            imgMsg.Source = new BitmapImage(new Uri(String.Format("../Resources/Messages/{0}.jpg", picture), UriKind.Relative));
        }

        private void Translate()
        {
            btnOk.Content = EnglishStrings.Ok.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
        }

        public void HideCancelButton()
        {
            btnOk.Margin = btnCancel.Margin;
            btnCancel.Visibility = System.Windows.Visibility.Collapsed;
            btnOk.IsCancel = true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public static void ShowDialog(string title, string text, MessagePicture picture, Action<bool?> onClose)
        {
            CustomMessageBox messageBox = new CustomMessageBox(title, text, picture);
            messageBox.ShowDialog(() =>
                {
                    if (onClose != null)
                        onClose(messageBox.DialogResult);
                });
        }

        public static void ShowDialog(string title, string text, MessagePicture picture)
        {
            CustomMessageBox messageBox = new CustomMessageBox(title, text, picture);
            messageBox.HideCancelButton();
            messageBox.ShowDialog();
        }
    }
}