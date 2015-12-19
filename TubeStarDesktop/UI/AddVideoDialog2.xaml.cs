using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for AddVideoDialog2.xaml
    /// </summary>
    public partial class AddVideoDialog2 : ChildWindow
    {
        private List<VideoAttribute> _currentAttributes;
        private bool _ignoreSelectEvent;

        public Video Video { get; private set; }

        public AddVideoDialog2(Video video)
        {
            InitializeComponent();
            Video = video;
            Translate();

            _currentAttributes = new List<VideoAttribute>();
            PopulateAttributeButtons();
            UpdateAttributePointLabel(Player.Current.VideoAttributePoints);

            UpdateButtons();
        }

        private void Translate()
        {
            Caption = EnglishStrings.AddVideo.Translate();
            lblAttributeSelect.Text = EnglishStrings.Attributes.Translate() + ":";
            btnOk.Content = EnglishStrings.Ok.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
        }

        private void PopulateAttributeButtons()
        {
            btnCats.VideoAttribute = VideoAttributes.Cats;
            btnCopycat.VideoAttribute = VideoAttributes.CopyCat;
            btnFanBoyBat.VideoAttribute = VideoAttributes.FanBoyBait;
            btnGenreBuster.VideoAttribute = VideoAttributes.GenreBuster;
            btnHypnotic.VideoAttribute = VideoAttributes.Hypnotic;
            btnLearnFromMistakes.VideoAttribute = VideoAttributes.LearnFromMistakes;
            btnMemetic.VideoAttribute = VideoAttributes.Memetic;
            btnProductPlacement.VideoAttribute = VideoAttributes.ProductPlacement;
            btnSecondTime.VideoAttribute = VideoAttributes.SecondTime;
            btnSoBad.VideoAttribute = VideoAttributes.SoBad;
            btnCrowdfunding.VideoAttribute = VideoAttributes.Crowdfunding;
            btnAboveBoard.VideoAttribute = VideoAttributes.AboveBoard;
        }

        private void UpdateAttributePointLabel(int count)
        {
            lblAttributeSelect.Text = String.Format("{0}: ({1} {2})", EnglishStrings.Attributes.Translate(), count, EnglishStrings.PointsLeft.Translate());
        }

        private void SelectButton_SelectedChanged(object sender, EventArgs e)
        {
            if (_ignoreSelectEvent)
                return;

            var selectButton = sender as SelectButton;
            if (selectButton != null)
            {
                if (selectButton.Selected)
                    _currentAttributes.Add(selectButton.VideoAttribute);
                else
                    _currentAttributes.Remove(selectButton.VideoAttribute);

                int totalAttributePoints = 0;
                _currentAttributes.ForEach(v => totalAttributePoints += v.Cost);

                if (totalAttributePoints > Player.Current.VideoAttributePoints)
                {
                    _ignoreSelectEvent = true;
                    selectButton.Selected = false;
                    _currentAttributes.Remove(selectButton.VideoAttribute);
                    _ignoreSelectEvent = false;
                }
                else
                {
                    UpdateAttributePointLabel(Player.Current.VideoAttributePoints - totalAttributePoints);
                }

                UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            int totalCost = 0;
            _currentAttributes.ForEach(a => totalCost += a.Cost);

            foreach (SelectButton button in attributeGrid.Children)
            {
                if (!button.Selected && button.VideoAttribute.Cost + totalCost > Player.Current.VideoAttributePoints)
                {
                    button.IsEnabled = false;
                }
                else
                {
                    button.IsEnabled = true;
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Video.Attributes = _currentAttributes;
            Video.FetchRandomImage();

            Settings.LastCategory = Video.Category;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}