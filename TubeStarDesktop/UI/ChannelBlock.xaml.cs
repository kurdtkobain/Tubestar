using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for ChannelBlock.xaml
    /// </summary>
    public partial class ChannelBlock : UserControl
    {
        public event EventHandler ChannelClick;
        public event EventHandler EditClick;

        private Channel _channel;
        public Channel Channel
        {
            get { return _channel; }
            private set
            {
                _channel = value;
                if (_channel != null)
                {
                    UpdateText();
                    if (_channel == Channel.UnreleasedVideos)
                    {
                        btnEdit.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                rootGrid.Background = new SolidColorBrush(_selected ? 
                    (Channel != null && Channel.IsRivalChannel ? Colors.DodgerBlue : Colors.Crimson) : 
                    Colors.Silver);
            }
        }

        public ChannelBlock(Channel channel, bool selected)
        {
            InitializeComponent();
            Channel = channel;
            Selected = selected;
            Translate();
        }

        public void HideEdit()
        {
            btnEdit.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Translate()
        {
            txtEdit.Text = EnglishStrings.Edit.Translate();
        }

        private void Channel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.PlayAnimation(() =>
                {
                    if (ChannelClick != null)
                        ChannelClick(this, EventArgs.Empty);
                });
        }

        public void UpdateText()
        {
            if (_channel != null)
            {
                channelTextBlock.Text = _channel.IsSuspended ? EnglishStrings.Suspended.Translate().ToUpper() : _channel.Name;
                txtVideoCount.Text = _channel.IsSuspended ? "" : String.Format("{0}: {1}", EnglishStrings.Videos.Translate(), _channel.Videos.Count);
            }
        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (EditClick != null)
                EditClick(this, EventArgs.Empty);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.LightGray;
            if (!_selected)
            {
                rootGrid.Background = new SolidColorBrush(Colors.DarkGray);
            }    
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Transparent;
            if (!_selected)
            {
                rootGrid.Background = new SolidColorBrush(Colors.Silver);
            }  
        }
    }
}