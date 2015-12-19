using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for TodoTask.xaml
    /// </summary>
    public partial class TodoTask : UserControl
    {
        public event EventHandler TaskClick;
        public event EventHandler CancelTaskClick;

        private Task _task;
        public Task Task
        {
            get { return _task; }
            private set
            {
                _task = value;
                todoGrid.Opacity = (_task == null) ? 0 : 1;
                if (_task != null)
                {
                    todoGrid.Background = new SolidColorBrush(_task.Color);
                    UpdateText();
                }
            }
        }

        public TodoTask(Task task)
        {
            InitializeComponent();
            Task = task;
        }

        private void Appointment_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.PlayAnimation(() =>
                {
                    if (TaskClick != null)
                        TaskClick(this, EventArgs.Empty);
                });
        }

        public void UpdateText()
        {
            if (_task != null)
            {
                appointmentTextBlock.Text = _task.Name;
                //txtHoursLeft.Text = String.Format("{0}: {1}", EnglishStrings.HoursLeft.Translate(), _task.HoursLeft);
                txtHoursLeft.Text = String.Format(EnglishStrings.HoursLeft.Translate(), _task.HoursLeft);
            }
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CancelTaskClick != null)
                CancelTaskClick(this, EventArgs.Empty);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.LightGray;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Transparent;
        }
    }
}