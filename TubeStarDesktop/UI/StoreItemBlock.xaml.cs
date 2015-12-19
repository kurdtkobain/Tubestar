using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for StoreItemBlock.xaml
    /// </summary>
    public partial class StoreItemBlock : UserControl
    {
        public event Action PurchaseMade;

        private StoreItem _storeItem;
        public StoreItem StoreItem
        {
            get { return _storeItem; }
            private set
            {
                _storeItem = value;
                if (_storeItem != null)
                {
                    txtName.Text = _storeItem.Name;
                    txtDescription.Text = _storeItem.Description;
                    txtPrice.Text = _storeItem.Cost.ToCurrencyString();
                    this.IsEnabled = _storeItem.Purchased ? false : true;
                    imgItem.Source = new BitmapImage(new Uri(String.Format("../Resources/StoreItems/{0}.jpg", _storeItem.ImageName), UriKind.Relative));
                }
            }
        }

        public StoreItemBlock(StoreItem item)
        {
            InitializeComponent();
            StoreItem = item;
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (Player.Current.Money - _storeItem.Cost < 0)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.LowCashHeader.Translate(), EnglishStrings.LowCashMessage.Translate(), MessagePicture.Money);
                return;
            }

            Player.Current.Money -= _storeItem.Cost;
            _storeItem.Purchased = true;
            this.IsEnabled = false;

            if (_storeItem == StoreItems.Current.Loan)
            {
                Player.Current.LoanPayOff = (StoreItems.Current.Loan.Payout * (100 + StoreItems.Current.Loan.Interest) / 100);
                Player.Current.Money += StoreItems.Current.Loan.Payout;
            }
        }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.PlayAnimation(() =>
            {
                if (Player.Current.Money - _storeItem.Cost < 0)
                {
                    CustomMessageBox.ShowDialog(EnglishStrings.LowCashHeader.Translate(), EnglishStrings.LowCashMessage.Translate(), MessagePicture.Money);
                    return;
                }

                CustomMessageBox.ShowDialog(EnglishStrings.Buy.Translate(), String.Format(EnglishStrings.BuyItem.Translate(), _storeItem.Cost.ToCurrencyString()), MessagePicture.Question, result =>
                {
                    if (result == true)
                    {
                        Player.Current.Money -= _storeItem.Cost;
                        _storeItem.Purchased = true;

                        if (_storeItem == StoreItems.Current.Loan)
                        {
                            Player.Current.LoanPayOff = (StoreItems.Current.Loan.Payout * (100 + StoreItems.Current.Loan.Interest) / 100);
                            Player.Current.Money += StoreItems.Current.Loan.Payout;
                        }

                        if (PurchaseMade != null)
                            PurchaseMade();
                    }
                });
            });
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.DodgerBlue;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Transparent;
        }
    }
}