using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for NewGameDialog.xaml
    /// </summary>
    public partial class NewGameDialog : ChildWindow
    {
        public event Action CasualClicked;
        public event Action CareerClicked;
        public event Action UltraClicked;

        public NewGameDialog()
        {
            InitializeComponent();
            Translate();
            commonsButton.IsChecked = Settings.UseCreativeCommons;
        }

        private void Translate()
        {
            Caption = EnglishStrings.NewGame.Translate();
            txtCasual.Text = EnglishStrings.Casual.Translate();
            txtChallenge.Text = EnglishStrings.Challenge.Translate();
            txtUltra.Text = EnglishStrings.Ultra.Translate();
            commonsText1.Text = EnglishStrings.UseCreativeCommons.Translate();
        }

        private void Casual_Click(object sender, RoutedEventArgs e)
        {
            UpdateSettings();
            if (CasualClicked != null)
            {
                CasualClicked();
            }
        }

        private void Career_Click(object sender, RoutedEventArgs e)
        {
            UpdateSettings();
            if (CareerClicked != null)
            {
                CareerClicked();
            }
        }

        private void Ultra_Click(object sender, RoutedEventArgs e)
        {
            UpdateSettings();
            if (UltraClicked != null)
            {
                UltraClicked();
            }
        }

        private void UpdateSettings()
        {
            Settings.UseCreativeCommons = (commonsButton.IsChecked == true);
            Properties.Settings.Default.UseCreativeCommons = Settings.UseCreativeCommons;
        }
    }
}