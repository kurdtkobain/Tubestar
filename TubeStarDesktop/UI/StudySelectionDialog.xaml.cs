using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for StudySelectionDialog.xaml
    /// </summary>
    public partial class StudySelectionDialog : ChildWindow
    {
        public Study ChosenStudy { get; private set; }

        public StudySelectionDialog()
        {
            InitializeComponent();
            Translate();

            PopulateStudyButtons();
        }

        private void Translate()
        {
            Caption = EnglishStrings.StudySelection.Translate();
            btnCancel.Content = EnglishStrings.Cancel.Translate();
        }

        private void PopulateStudyButtons()
        {
            foreach (var study in Studies.Current.All)
            {
                var studyCost = Player.Current.ChallengeMode ? study.Cost * 1.5 : study.Cost;

                Button button = new Button();
                button.Margin = new Thickness(0, 2, 0, 2);
                button.Height = 60;
                button.HorizontalAlignment = HorizontalAlignment.Stretch;
                button.HorizontalContentAlignment = HorizontalAlignment.Left;

                string details = String.Format("{0}: {1}\t{2}: {3}", EnglishStrings.Cost.Translate(), studyCost.ToCurrencyString(), EnglishStrings.HoursToComplete.Translate(), study.HoursToComplete); ;
                button.Content = CreateButtonContent(study.Name, details);

                button.Click += (s, e) =>
                {
                    if (Player.Current.Money - studyCost < 0)
                    {
                        CustomMessageBox.ShowDialog(EnglishStrings.LowCashHeader.Translate(), EnglishStrings.LowCashMessage.Translate(), MessagePicture.Money);
                        return;
                    }

                    ChosenStudy = study;
                    Player.Current.Money -= studyCost;
                    DialogResult = true;
                };

                //Already studied
                if (study.IsCompleted)
                {
                    button.IsEnabled = false;
                    button.Content = CreateButtonContent(study.Name, EnglishStrings.AlreadyCompleted.Translate());
                }

                //Prerequisite not met
                else if (study.Prerequisite != null && !study.Prerequisite.IsCompleted)
                {
                    button.IsEnabled = false;
                    button.Content = CreateButtonContent(study.Name, EnglishStrings.PrerequisitesNotMet.Translate());
                }

                //In progresss
                foreach (var checkTask in Player.Current.TasksInProgress)
                {
                    if (checkTask == study)
                    {
                        button.IsEnabled = false;
                        button.Content = CreateButtonContent(study.Name, EnglishStrings.AlreadyEnrolled.Translate());
                    }
                }

                studyButtonStackPanel.Children.Add(button);
            }
        }

        private StackPanel CreateButtonContent(string name, string details)
        {
            TextBlock tbName = new TextBlock();
            tbName.FontSize = 18;
            tbName.FontWeight = FontWeights.Bold;
            tbName.Text = name;

            TextBlock tbDetails = new TextBlock();
            tbDetails.Text = details;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.Children.Add(tbName);
            stackPanel.Children.Add(tbDetails);

            return stackPanel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}