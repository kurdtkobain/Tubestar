using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for OnlineStoreControl.xaml
    /// </summary>
    public partial class OnlineStoreControl : UserControl
    {
        public OnlineStoreControl()
        {
            InitializeComponent();
            Translate();
        }

        private void Translate()
        {
        }

        public void Update()
        {
            itemPanel.Children.Clear();
            foreach (var item in StoreItems.Current.All)
            {
                if (!item.Purchased)
                {
                    var storeBlock = new StoreItemBlock(item);
                    storeBlock.PurchaseMade += StoreBlock_PurchaseMade;
                    itemPanel.Children.Insert(0, storeBlock);
                }
            }
        }

        private void StoreBlock_PurchaseMade()
        {
            Update();
        }
    }
}