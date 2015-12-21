using System;
using System.Windows.Controls;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for SelectButton.xaml
    /// </summary>
    public partial class SelectButton : UserControl
    {
        public event EventHandler SelectedChanged;

        private VideoAttribute _videoAttribute;

        public VideoAttribute VideoAttribute
        {
            get { return _videoAttribute; }
            set
            {
                _videoAttribute = value;
                txtButtonText.Text = String.Format("{0}", _videoAttribute.Name);
                txtDetails.Text = _videoAttribute.Description;

                string points = String.Empty;
                for (int i = 0; i < _videoAttribute.Cost; i++)
                {
                    points += "•";
                }
                txtPoints.Text = points;
            }
        }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                btnToggle.IsChecked = value;
                if (SelectedChanged != null)
                    SelectedChanged(this, EventArgs.Empty);
            }
        }

        public SelectButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Selected = !Selected;
        }
    }
}