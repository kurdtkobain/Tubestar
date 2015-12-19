using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    public static class DialogHelper
    {
        private static Grid RootElement
        {
            get
            {
                return LogicalTreeHelper.FindLogicalNode(Application.Current.MainWindow, "LayoutRoot") as Grid;
            }
        }

        public static void ShowDialog(this ChildWindow window, Action onClose = null)
        {
            RootElement.Children.Add(window);
            window.IsModal = true;
            window.WindowStartupLocation = Xceed.Wpf.Toolkit.WindowStartupLocation.Center;
            Grid.SetRowSpan(window, 10);
            Grid.SetColumnSpan(window, 10);
            window.Closed += (s, ea) =>
            {
                RootElement.Children.Remove(window);
                if (onClose != null)
                    onClose();
            };
            window.Show();
            window.Focus();
        }
    }
}