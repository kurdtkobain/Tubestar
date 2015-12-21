using System;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : ChildWindow
    {
        public string Username
        {
            get { return txtName.Text; }
        }

        public string Token
        {
            get { return txtToken.Text; }
        }

        public LoginDialog()
        {
            InitializeComponent();
            Translate();
            FocusedElement = txtName;
        }

        private void Translate()
        {
            Caption = EnglishStrings.Login.Translate();
            lblUserName.Text = EnglishStrings.Username.Translate() + ":";
            lblToken.Text = EnglishStrings.Token.Translate() + ":";
            btnOk.Content = EnglishStrings.Ok.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingUserName.Translate(), MessagePicture.Puzzle);
                return;
            }

            if (String.IsNullOrEmpty(txtToken.Text))
            {
                CustomMessageBox.ShowDialog(EnglishStrings.MissingValueHeader.Translate(), EnglishStrings.MissingToken.Translate(), MessagePicture.Puzzle);
                return;
            }

            Settings.PlayerName = txtName.Text;

            WebClientHelpers.Download<AuthUserResult>(AuthUserManager.GetAuthUserUri(txtName.Text, txtToken.Text), Client_DownloadStringCompleted, null);
            btnOk.IsEnabled = false;
        }

        private void Client_DownloadStringCompleted(AuthUserResult result)
        {
            if (result.Response.Success)
            {
                this.DialogResult = true;
            }
            else
            {
                CustomMessageBox.ShowDialog(EnglishStrings.Failure.Translate(), EnglishStrings.LoginFailure.Translate(), MessagePicture.Error);
                btnOk.IsEnabled = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}