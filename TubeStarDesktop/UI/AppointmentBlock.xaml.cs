using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for AppointmentBlock.xaml
    /// </summary>
    public partial class AppointmentBlock : UserControl
    {
        public event EventHandler AppointmentClick;

        public string Hour { get; set; }
        public string Minute { get; set; }

        private Task _task;

        public Task Task
        {
            get { return _task; }
            set
            {
                _task = value;

                bool isNull = (_task == null);

                appointmentGrid.Background = new SolidColorBrush(isNull ? Colors.Transparent : _task.Color);
                txtTime.Text = String.Format("{0} {1}", Hour, Minute);
                appointmentTextBlock.Text = isNull ? "-" : _task.Name;
                txtTime.Foreground = appointmentTextBlock.Foreground = isNull ? Brushes.Black : Brushes.WhiteSmoke;
            }
        }

        public AppointmentBlock()
        {
            InitializeComponent();
        }

        private void AppointmentBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.PlayAnimation(() =>
            {
                if (AppointmentClick != null)
                    AppointmentClick(this, EventArgs.Empty);
            });
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