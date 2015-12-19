using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for CreditsDialog.xaml
    /// </summary>
    public partial class CreditsDialog : ChildWindow
    {
        public CreditsDialog()
        {
            InitializeComponent();
            Caption = EnglishStrings.Credits.Translate();
            btnOk.Content = EnglishStrings.Ok.Translate();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void imgYouTube_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.youtube.com");
        }

        private void imgPixabay_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pixabay.com");
        }

        private void imgStockXchang_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.freeimages.com/");
        }

        private void imgIcons8_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://icons8.com/");
        }
    }
}