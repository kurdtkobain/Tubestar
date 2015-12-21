using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for TitlePage.xaml
    /// </summary>
    public partial class TitlePage : UserControl
    {
        public event Action ContinueGameClicked;

        public event Action<bool, bool> NewGameClicked;

        private string _steamUrl;

        public TitlePage()
        {
            InitializeComponent();

            UpdateLoginButton();
            Refresh();
            CheckVersion();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGameDialog dialog = new NewGameDialog();
            dialog.CasualClicked += () =>
            {
                if (NewGameClicked != null)
                    NewGameClicked(false, false);
            };
            dialog.CareerClicked += () =>
            {
                if (NewGameClicked != null)
                    NewGameClicked(true, false);
            };
            dialog.UltraClicked += () =>
            {
                if (NewGameClicked != null)
                    NewGameClicked(true, true);
            };
            dialog.ShowDialog();
        }

        private void ContinueGame_Click(object sender, RoutedEventArgs e)
        {
            if (ContinueGameClicked != null)
                ContinueGameClicked();
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            if (!TrophyManager.HasTrophy(Trophy.Pupil))
                TrophyManager.UnlockTrophy(Trophy.Pupil);

            System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=oKfNSm1SLSQ");
        }

        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            CreditsDialog dialog = new CreditsDialog();
            dialog.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GJLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog login = new LoginDialog();
            login.ShowDialog(() =>
            {
                if (login.DialogResult == true)
                {
                    Settings.GameJoltLogin = login.Username;
                    Settings.GameJoltToken = login.Token;
                    UpdateLoginButton();

                    TrophyManager.UnlockTrophy(Trophy.Upgrade);
                }
            });
        }

        private void UpdateLoginButton()
        {
            if (!String.IsNullOrEmpty(Settings.GameJoltLogin))
            {
                btnLogin.Visibility = Visibility.Collapsed;
            }
        }

        public void Refresh()
        {
            btnContinue.IsEnabled = System.IO.File.Exists(SaveLoadHelper.SaveFile);
            Translate();
        }

        private void Translate()
        {
            btnContinue.Content = EnglishStrings.ContinueGame.Translate();
            btnNewGame.Content = EnglishStrings.NewGame.Translate();
            btnTutorial.Content = EnglishStrings.Tutorial.Translate();
            btnCredits.Content = EnglishStrings.Credits.Translate();
            imgCustom.ToolTip = EnglishStrings.Mods.Translate();
            btnExit.Content = EnglishStrings.Exit.Translate();
            txtLogin.Text = EnglishStrings.Login.Translate();

            TranslatedBy.Text = "";
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                if (Properties.Settings.Default.Language.Contains("France"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.facebook.com/soldierx3softwares")
                    };
                    hyperLink.Inlines.Add("SoldierX3");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);

                    TranslatedBy.Inlines.Add(" / MrKikou / Xcvazer");
                }
                if (Properties.Settings.Default.Language.Contains("Germany"))
                {
                    TranslatedBy.Text = "iKlikla / LysandriosLP / Nick Benz / Vycer";
                }
                if (Properties.Settings.Default.Language.Contains("Sweden"))
                {
                    TranslatedBy.Text = "Swertayy";
                }
                if (Properties.Settings.Default.Language.Contains("Russia"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://www.youtube.com/SamaraGamerForever")
                    };
                    hyperLink.Inlines.Add("SamaraGamer");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink2 = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.youtube.com/user/MixPremiumGames")
                    };
                    hyperLink2.Inlines.Add("Mix");
                    hyperLink2.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink2);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink3 = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://vk.com/egorandstas")
                    };
                    hyperLink3.Inlines.Add("Virt131");
                    hyperLink3.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink3);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink4 = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://stalkerzone99.wix.com/kodu-game-lab")
                    };
                    hyperLink4.Inlines.Add("The_Chris");
                    hyperLink4.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink4);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink5 = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://www.youtube.com/user/dima0325feed")
                    };
                    hyperLink5.Inlines.Add("dima0325");
                    hyperLink5.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink5);

                    TranslatedBy.Inlines.Add(" / Lolycon / Мария Шварц / Noise / Сергей Якшич / Virt131 / Илья / Grift100 / chocolater12 / CheessteR / Юрий");
                }
                if (Properties.Settings.Default.Language.Contains("Poland"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.facebook.com/norbertgrom.gmr")
                    };
                    hyperLink.Inlines.Add("Norbertgrom");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);
                    TranslatedBy.Inlines.Add(" / Kaspartos / Hkg / Siwy / Michał / Szejker Gejmer / benk86 / LokosQuixPL / Jakub / Zniewidzialny / Maciej Bluz / MetusiaGaming / Kasper94518");
                }
                if (Properties.Settings.Default.Language.Contains("Thailand"))
                {
                    TranslatedBy.Text = "Suksid suksattayanon";
                }
                if (Properties.Settings.Default.Language.Contains("Turkey"))
                {
                    TranslatedBy.Text = "Tibet / Kardos / HASAN / Görkem";
                }
                if (Properties.Settings.Default.Language.Contains("China"))
                {
                    TranslatedBy.Text = "Shadow";
                }
                if (Properties.Settings.Default.Language.Contains("Netherlands"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://daniellichthart.nl")
                    };
                    hyperLink.Inlines.Add("Daniel Lichthart");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);
                }
                if (Properties.Settings.Default.Language.Contains("Spain"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.youtube.com/channel/UCZkdV4jaZMWssj5ZRuwjRmQ")
                    };
                    hyperLink.Inlines.Add("Jorge");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink2 = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://www.youtube.com/user/elcompjr1")
                    };
                    hyperLink2.Inlines.Add("ElcompJR");
                    hyperLink2.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink2);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink3 = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.youtube.com/user/TheCalaco44")
                    };
                    hyperLink3.Inlines.Add("TheCalaco44");
                    hyperLink3.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink3);

                    TranslatedBy.Inlines.Add(" / Marcelo Sepúlveda / Martin / Sebastian Ramirez / Tamara / Aimar / Vinicio Calderón / Fede / Patricia Matamala / Carlos / UnpopularPage24 / Rebeca Chocobar");
                }
                if (Properties.Settings.Default.Language.Contains("Albania"))
                {
                    TranslatedBy.Text = "Leonor Rustemi";
                }
                if (Properties.Settings.Default.Language.Contains("Romania"))
                {
                    TranslatedBy.Text = "Mihaly Vegh";
                }
                if (Properties.Settings.Default.Language.Contains("Indonesia"))
                {
                    TranslatedBy.Text = "Alexander Bimo";
                }
                if (Properties.Settings.Default.Language.Contains("Hungary"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.youtube.com/user/smileyhead1500")
                    };
                    hyperLink.Inlines.Add("Smileyhead");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);
                    TranslatedBy.Inlines.Add(" / Niekay");
                }
                if (Properties.Settings.Default.Language.Contains("Italy"))
                {
                    TranslatedBy.Text = "Mar / RickyWiiU / Mari";
                }
                if (Properties.Settings.Default.Language.Contains("Czech"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.youtube.com/user/MESO666601")
                    };
                    hyperLink.Inlines.Add("DarkSiders");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);

                    TranslatedBy.Inlines.Add(" / ");

                    Hyperlink hyperLink2 = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.youtube.com/user/WeAreProudLlamas")
                    };
                    hyperLink2.Inlines.Add("KorduzCZ");
                    hyperLink2.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink2);

                    TranslatedBy.Inlines.Add(" / Nulmex / Martin / Ondřej Fiala / Supercreep / Zezik");
                }
                if (Properties.Settings.Default.Language.Contains("Brazil"))
                {
                    TranslatedBy.Text = "Domingoss / Fabio";
                }
                if (Properties.Settings.Default.Language.Contains("Portugal"))
                {
                    TranslatedBy.Text = "Neto / Hutgar / Adley / mebetop / GhostTuga01";
                }
                if (Properties.Settings.Default.Language.Contains("Georgia"))
                {
                    TranslatedBy.Inlines.Clear();
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri("https://www.facebook.com/nika.kantidze.1")
                    };
                    hyperLink.Inlines.Add("Nikoloz Kantidze");
                    hyperLink.RequestNavigate += hyperLink_RequestNavigate;
                    TranslatedBy.Inlines.Add(hyperLink);
                }

                //DENMARK -  Thomas Olsen (www.fishao.com)
            }
        }

        private void hyperLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void imgGeorgia_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Georgia");
            Refresh();
        }

        private void imgPoland_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Poland");
            Refresh();
        }

        private void imgRussia_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Russia");
            Refresh();
        }

        private void imgUK_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("English (default)");
            Refresh();
        }

        private void imgFrance_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("France");
            Refresh();
        }

        private void imgGermany_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Germany");
            Refresh();
        }

        private void imgSweden_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Sweden");
            Refresh();
        }

        private void imgThailand_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Thailand");
            Refresh();
        }

        private void imgTurkey_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Turkey");
            Refresh();
        }

        private void imgChina_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("China");
            Refresh();
        }

        private void imgNetherlands_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Netherlands");
            Refresh();
        }

        private void imgSpain_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Spain");
            Refresh();
        }

        private void imgAlbania_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Albania");
            Refresh();
        }

        private void imgRomania_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Romania");
            Refresh();
        }

        private void imgIndonesia_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Indonesia");
            Refresh();
        }

        private void imgHungary_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Hungary");
            Refresh();
        }

        private void imgItaly_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Italy");
            Refresh();
        }

        private void imgCzech_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Czech");
            Refresh();
        }

        private void imgBrazil_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Brazil");
            Refresh();
        }

        private void imgPortugal_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Languages.SetLanguage("Portugal");
            Refresh();
        }

        private void imgCustom_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            ofd.Filter = "XML File|*.xml";
            if (ofd.ShowDialog(Application.Current.MainWindow) == true)
            {
                if (File.Exists(ofd.FileName))
                {
                    Languages.ReadLanguage(ofd.FileName);
                    Refresh();
                }
            }
        }

        private void CheckVersion()
        {
            var uri = DataStoreManager.GetDataUri("Version");
            WebClientHelpers.Download<GetDataResult>(uri, (s) =>
            {
                if (s.Response.Success)
                {
                    var latestVersion = Version.Parse(s.Response.Data);
                    var thisVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                    if (thisVersion < latestVersion)
                    {
                        imgUpdate.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }, null);
        }

        private void imgUpdate_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gamejolt.com/games/strategy-sim/tubestar/11858/");
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gamejolt.com/games/strategy-sim/tubestar/11858/");
        }
    }
}