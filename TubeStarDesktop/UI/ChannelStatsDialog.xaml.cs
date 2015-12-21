using OxyPlot;
using OxyPlot.Series;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for VideoStatsDialog.xaml
    /// </summary>
    public partial class ChannelStatsDialog : ChildWindow
    {
        public ChannelStatsDialog(Channel channel)
        {
            InitializeComponent();
            Translate();
            Populate(channel);
        }

        private void Translate()
        {
            Caption = EnglishStrings.Stats.Translate();
            btnOk.Content = EnglishStrings.Ok.Translate();
        }

        private void Populate(Channel channel)
        {
            PopulateSubscribers(channel);
            PopulateIncome(channel);
        }

        private void PopulateSubscribers(Channel channel)
        {
            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = EnglishStrings.SubscriberHistory.Translate();
            nameBlock.FontSize = 16;
            nameBlock.Margin = new Thickness(5, 0, 5, 0);
            plotParent.Children.Add(nameBlock);

            OxyPlot.Wpf.Plot plot = new OxyPlot.Wpf.Plot();
            plot.Height = 200;

            var plotModel1 = new PlotModel();
            var viewSeries = new LineSeries(OxyColors.Blue);
            int count = 1;
            foreach (var subscriber in channel.SubscribersOverTime)
            {
                viewSeries.Points.Add(new DataPoint(count++, subscriber));
            }
            plotModel1.Series.Add(viewSeries);
            plot.Model = plotModel1;
            plotParent.Children.Add(plot);
        }

        private void PopulateIncome(Channel channel)
        {
            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = EnglishStrings.DailyIncome.Translate();
            nameBlock.FontSize = 16;
            nameBlock.Margin = new Thickness(5, 0, 5, 0);
            plotParent.Children.Add(nameBlock);

            OxyPlot.Wpf.Plot plot = new OxyPlot.Wpf.Plot();
            plot.Height = 200;

            var plotModel1 = new PlotModel();
            var incomeSeries = new LineSeries(OxyColors.Blue);
            int count = 1;
            double baseIncome = 0;
            foreach (var income in channel.IncomeOverTime)
            {
                baseIncome += income;
                incomeSeries.Points.Add(new DataPoint(count++, baseIncome));
            }
            plotModel1.Series.Add(incomeSeries);

            var expenseSeries = new LineSeries(OxyColors.Red);
            count = 1;
            double baseExpenses = 0;
            foreach (var expense in channel.ExpensesOverTime)
            {
                baseExpenses += expense;
                expenseSeries.Points.Add(new DataPoint(count++, baseExpenses));
            }
            plotModel1.Series.Add(expenseSeries);
            plot.Model = plotModel1;
            plotParent.Children.Add(plot);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}