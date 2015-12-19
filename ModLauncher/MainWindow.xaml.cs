using ModLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var data = GetData();
            cmbRivalMod.ItemsSource = data;
            cmbRivalMod.DisplayMemberPath = "Value";
            cmbRivalMod.SelectedValuePath = "Key";

            if (!String.IsNullOrEmpty(Settings.Default.RivalModPath))
            {
                cmbRivalMod.SelectedValue = Settings.Default.RivalModPath;
            }
        }

        private Dictionary<string, string> GetData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            var baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.EnumerateFiles(System.IO.Path.Combine(baseDir, "Mods\\Custom TopTubeStars"), "*.xml", SearchOption.AllDirectories))
            {
                data[file] = System.IO.Path.GetFileNameWithoutExtension(file);
            }

            List<KeyValuePair<string, string>> sortTemp = data.ToList();
            sortTemp.Sort((l, r) => l.Value.CompareTo(r.Value));

            return sortTemp.ToDictionary((s) => s.Key, (s) => s.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.RivalModPath = (string)cmbRivalMod.SelectedValue;
            Settings.Default.Save();

            Process.Start("TubeStar.exe", String.Format("RIVALMOD \"{0}\"", Settings.Default.RivalModPath));
            Application.Current.Shutdown();
        }
    }
}
