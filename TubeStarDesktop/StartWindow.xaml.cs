using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private TitlePage _titlePage;
        private MainPage _mainPage;

        public StartWindow()
        {
            InitializeComponent();

            if (!String.IsNullOrEmpty(Properties.Settings.Default.CustomModPath))
            {
                Languages.ReadLanguage(Properties.Settings.Default.CustomModPath);
            }
            else if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                Languages.SetLanguage(Properties.Settings.Default.Language);
            }
            else
            {
                if (CultureInfo.CurrentCulture.Name.StartsWith("ru-"))
                {
                    Languages.SetLanguage("Russia");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("pl-"))
                {
                    Languages.SetLanguage("Poland");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("de-"))
                {
                    Languages.SetLanguage("Germany");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("sv-"))
                {
                    Languages.SetLanguage("Sweden");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("fr-"))
                {
                    Languages.SetLanguage("France");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("th-"))
                {
                    Languages.SetLanguage("Thailand");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("tr-"))
                {
                    Languages.SetLanguage("Turkey");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("zh-"))
                {
                    Languages.SetLanguage("China");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("nl-"))
                {
                    Languages.SetLanguage("Netherlands");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("es-"))
                {
                    Languages.SetLanguage("Spain");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("sq-"))
                {
                    Languages.SetLanguage("Albania");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("ro-"))
                {
                    Languages.SetLanguage("Romania");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("id-"))
                {
                    Languages.SetLanguage("Indonesia");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("hu-"))
                {
                    Languages.SetLanguage("Hungary");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("it-"))
                {
                    Languages.SetLanguage("Italy");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("cs-"))
                {
                    Languages.SetLanguage("Czech");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("pt-BR"))
                {
                    Languages.SetLanguage("Brazil");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("pt-"))
                {
                    Languages.SetLanguage("Portugal");
                }
                else if (CultureInfo.CurrentCulture.Name.StartsWith("ka-"))
                {
                    Languages.SetLanguage("Georgia");
                }
            }

            Settings.ListView = Properties.Settings.Default.ListView;
            Settings.UseCreativeCommons = Properties.Settings.Default.UseCreativeCommons;

            _titlePage = new TitlePage();
            _titlePage.NewGameClicked += TitlePage_NewGameClicked;
            _titlePage.ContinueGameClicked += TitlePage_ContinueGameClicked;
            SetContent(_titlePage);
        }

        private void CommunityControl_BackClick()
        {
            SetContent(_titlePage);
        }

        private void SetContent(UserControl page)
        {
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(page);
        }

        private void TitlePage_NewGameClicked(bool careerMode, bool ultraMode)
        {
            VideoViewer.Reset();
            Player.Current.Reset();
            Player.Current.ChallengeMode = careerMode;
            Player.Current.UltraMode = ultraMode;

            _mainPage = new MainPage();
            _mainPage.GameLose += MainPage_GameLose;
            _mainPage.GameExit += MainPage_GameExit;
            _mainPage.GameDeath += MainPage_GameDeath;
            SetContent(_mainPage);
        }

        private void TitlePage_ContinueGameClicked()
        {
            VideoViewer.Reset();
            Player.Current.Reset();
            this.IsEnabled = false;
            SaveLoadHelper.Load(SaveLoadHelper.SaveFile);
            this.IsEnabled = true;

            _mainPage = new MainPage();
            _mainPage.GameLose += MainPage_GameLose;
            _mainPage.GameExit += MainPage_GameExit;
            _mainPage.GameDeath += MainPage_GameDeath;
            SetContent(_mainPage);
            Player.Current.Money = Player.Current.Money;
        }

        private void MainPage_GameDeath()
        {
            SetContent(_titlePage);
            _titlePage.Refresh();
            CustomMessageBox.ShowDialog(EnglishStrings.Death.Translate(), EnglishStrings.RobotDeath.Translate(), MessagePicture.Robot);
        }

        private void MainPage_GameExit()
        {
            SetContent(_titlePage);
            _titlePage.Refresh();
        }

        private void MainPage_GameLose()
        {
            SetContent(_titlePage);
            _titlePage.Refresh();
            CustomMessageBox.ShowDialog(EnglishStrings.WhatALoser.Translate(), EnglishStrings.OutOfMoney.Translate(), MessagePicture.Money);
            TrophyManager.UnlockTrophy(Trophy.Loser);
        }
    }
}