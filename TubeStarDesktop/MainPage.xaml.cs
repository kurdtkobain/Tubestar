using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public event Action GameDeath;
        public event Action GameLose;
        public event Action GameExit;

        public MainPage()
        {
            InitializeComponent();
            Translate();
            Player.Current.MoneyChanged += () =>
            {
                Dispatcher.BeginInvoke(new Action(delegate()
                {
                    txtMoney.Text = Player.Current.Money.ToCurrencyString();
                }));
            };

            dailyPlanner.NewDayCompleted += () =>
            {
                this.IsEnabled = true;
                progress.Visibility = System.Windows.Visibility.Collapsed;

                if (videoManager.Visibility == System.Windows.Visibility.Visible)
                    videoManager.Update();

                if (rivalViewer.Visibility == System.Windows.Visibility.Visible)
                    rivalViewer.Update();

                txtNewDay.Text = String.Format("{0} {1}!", EnglishStrings.StartDay.Translate(), Player.Current.Iterations + 1);

                if (Player.Current.Iterations > 1 && Player.Current.Iterations % 3 == 0)
                {
                    RandomEvents.RunEvent();
                    videoManager.Update();
                    dailyPlanner.Update();
                    rivalViewer.Update();
                }

                CheckTrophies();
            };

            dailyPlanner.GameExit += () =>
            {
                if (GameLose != null)
                    GameLose();
            };
            dailyPlanner.Death += () =>
            {
                if (GameDeath != null)
                    GameDeath();
            };
        }

        private void Translate()
        {
            gridHelp.ToolTip = EnglishStrings.Help.Translate();
            gridExit.ToolTip = EnglishStrings.Exit.Translate();
            imgSave.ToolTip = EnglishStrings.SaveGame.Translate();
            txtDailyPlanner.Text = EnglishStrings.DailyPlanner.Translate();
            txtVideoPlanner.Text = EnglishStrings.VideoManager.Translate();
            txtOnlineStore.Text = EnglishStrings.OnlineStore.Translate();
            txtRivals.Text = EnglishStrings.TopTubeStars.Translate();
        }

        private void CheckTrophies()
        {
            foreach (var channel in Player.Current.Channels)
            {
                int heelCount = 0;
                int rantCount = 0;
                int nerdCount = 0;
                foreach (var video in channel.Videos)
                {
                    if (!TrophyManager.HasTrophy(Trophy.InternetFamous) && video.Views >= 100000)
                        TrophyManager.UnlockTrophy(Trophy.InternetFamous);

                    if (!TrophyManager.HasTrophy(Trophy.PropDepartment) && video.Cost == 400)
                        TrophyManager.UnlockTrophy(Trophy.PropDepartment);

                    if (!TrophyManager.HasTrophy(Trophy.WellHeeld) && video.Category == VideoCategory.Hauls && video.Name.ToLower().Contains("heels"))
                        heelCount++;

                    if (!TrophyManager.HasTrophy(Trophy.RantMaster) && video.Category == VideoCategory.Vlog && video.Name.ToLower().Contains("rant"))
                        rantCount++;

                    if (!TrophyManager.HasTrophy(Trophy.Procrastinator) && video.Category == VideoCategory.Gaming && video.Name.ToLower().Contains("nerd"))
                        nerdCount++;
                }

                if (heelCount >= 5)
                    TrophyManager.UnlockTrophy(Trophy.WellHeeld);

                if (rantCount >= 5)
                    TrophyManager.UnlockTrophy(Trophy.RantMaster);

                if (nerdCount >= 3)
                    TrophyManager.UnlockTrophy(Trophy.Procrastinator);
            }

            if (!TrophyManager.HasTrophy(Trophy.AptPupil))
            {
                bool completed = true;
                foreach (var study in Studies.Current.All)
                {
                    if (!study.IsCompleted)
                    {
                        completed = false;
                        break;
                    }
                }

                if (completed)
                    TrophyManager.UnlockTrophy(Trophy.AptPupil);
            }
        }

        private void btnDailyPlanner_Click(object sender, RoutedEventArgs e)
        {
            btnDailyPlanner.IsChecked = true;
            btnVideoPlanner.IsChecked = false;
            btnOnlineStore.IsChecked = false;
            btnRivals.IsChecked = false;

            dailyPlanner.Visibility = Visibility.Visible;
            videoManager.Visibility = Visibility.Collapsed;
            onlineStore.Visibility = Visibility.Collapsed;
            rivalViewer.Visibility = Visibility.Collapsed;
        }

        private void btnVideoPlanner_Click(object sender, RoutedEventArgs e)
        {
            btnDailyPlanner.IsChecked = false;
            btnVideoPlanner.IsChecked = true;
            btnOnlineStore.IsChecked = false;
            btnRivals.IsChecked = false;

            dailyPlanner.Visibility = Visibility.Collapsed;
            videoManager.Visibility = Visibility.Visible;
            onlineStore.Visibility = Visibility.Collapsed;
            rivalViewer.Visibility = Visibility.Collapsed;
            videoManager.Update();
        }

        private void btnOnlineStore_Click(object sender, RoutedEventArgs e)
        {
            btnDailyPlanner.IsChecked = false;
            btnVideoPlanner.IsChecked = false;
            btnOnlineStore.IsChecked = true;
            btnRivals.IsChecked = false;

            dailyPlanner.Visibility = Visibility.Collapsed;
            videoManager.Visibility = Visibility.Collapsed;
            onlineStore.Visibility = Visibility.Visible;
            rivalViewer.Visibility = Visibility.Collapsed;
            onlineStore.Update();
        }

        private void btnRivals_Click(object sender, RoutedEventArgs e)
        {
            btnDailyPlanner.IsChecked = false;
            btnVideoPlanner.IsChecked = false;
            btnOnlineStore.IsChecked = false;
            btnRivals.IsChecked = true;

            dailyPlanner.Visibility = Visibility.Collapsed;
            videoManager.Visibility = Visibility.Collapsed;
            onlineStore.Visibility = Visibility.Collapsed;
            rivalViewer.Visibility = Visibility.Visible;
            rivalViewer.Update();
        }

        private void btnNewDay_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            progress.Visibility = System.Windows.Visibility.Visible;

            dailyPlanner.NewDay();
            rivalViewer.NewDay();
        }

        private void Tutorial_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!TrophyManager.HasTrophy(Trophy.Pupil))
                TrophyManager.UnlockTrophy(Trophy.Pupil);

            System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=oKfNSm1SLSQ");
        }

        private void Save_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Directory.Exists(SaveLoadHelper.SaveDirectory))
            {
                Directory.CreateDirectory(SaveLoadHelper.SaveDirectory);
            }

            if (File.Exists(SaveLoadHelper.SaveFile))
            {
                CustomMessageBox.ShowDialog(EnglishStrings.OverwriteSave.Translate(), EnglishStrings.SaveExists.Translate(), MessagePicture.Question, (result) =>
                    {
                        if (result == true)
                            DoSave();
                    });
            }
            else
            {
                DoSave();
            }
        }

        private void DoSave()
        {
            dailyPlanner.Appointments.ForEach(a => a.HoursPutIn--);
            SaveLoadHelper.Save(SaveLoadHelper.SaveFile);
            dailyPlanner.Appointments.ForEach(a => a.HoursPutIn++);
        }

        private void Exit_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CustomMessageBox.ShowDialog(EnglishStrings.Confirm.Translate(), EnglishStrings.LeaveGame.Translate(), MessagePicture.Question, (result) =>
            {
                if (result == true)
                {
                    if (GameExit != null)
                        GameExit();
                }
            });
        }

        private void imgSave_MouseEnter(object sender, MouseEventArgs e)
        {
            imgSave.Source = new BitmapImage(new Uri("Resources/Disk_hover.png", UriKind.Relative));
        }

        private void imgSave_MouseLeave(object sender, MouseEventArgs e)
        {
            imgSave.Source = new BitmapImage(new Uri("Resources/Disk.png", UriKind.Relative));
        }

        private void gridHelp_MouseEnter(object sender, MouseEventArgs e)
        {
            gridHelp.Source = new BitmapImage(new Uri("Resources/Help_hover.png", UriKind.Relative));
        }

        private void gridHelp_MouseLeave(object sender, MouseEventArgs e)
        {
            gridHelp.Source = new BitmapImage(new Uri("Resources/Help.png", UriKind.Relative));
        }

        private void gridExit_MouseEnter(object sender, MouseEventArgs e)
        {
            gridExit.Source = new BitmapImage(new Uri("Resources/Exit_hover.png", UriKind.Relative));
        }

        private void gridExit_MouseLeave(object sender, MouseEventArgs e)
        {
            gridExit.Source = new BitmapImage(new Uri("Resources/Exit.png", UriKind.Relative));
        }
    }
}