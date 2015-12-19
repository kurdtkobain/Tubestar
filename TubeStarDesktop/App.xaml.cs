using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 1)
            {
                if (e.Args[0] == "RIVALMOD")
                {
                    Settings.RivalsModPath = e.Args[1];
                }
                else
                {
                    Settings.GameJoltLogin = e.Args[0];
                    Settings.GameJoltToken = e.Args[1];
                }
            }
            base.OnStartup(e);
            this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            TubeStar.Properties.Settings.Default.Save();
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("EXCEPTION:");
            sb.Append(e.Exception.Message);
            sb.AppendLine();
            sb.Append(e.Exception.StackTrace);
            sb.AppendLine();

            var innerException = e.Exception.InnerException;
            while (innerException != null)
            {
                sb.AppendLine("INNER EXCEPTION:");
                sb.Append(innerException.Message);
                sb.AppendLine();
                sb.Append(innerException.StackTrace);
                sb.AppendLine();
                innerException = innerException.InnerException;
            }

            File.WriteAllText("Crash Report.txt", sb.ToString());

            e.Handled = true;
            CustomMessageBox.ShowDialog(EnglishStrings.UnhandledError.Translate(), EnglishStrings.ExceptionText.Translate(), MessagePicture.Error, (result) =>
                {
                    if (result != true)
                        Application.Current.Shutdown();
                });
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {

        }
    }
}