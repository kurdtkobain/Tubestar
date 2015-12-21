using System;
using System.IO;

namespace TubeStar
{
    public static class SaveLoadHelper
    {
        public static string SaveDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + @"\Saves"; }
        }

        public static string SaveFile
        {
            get { return SaveDirectory + @"\save.tss"; }
        }

        public static void Save(string fileName)
        {
            try
            {
                SaveObj obj = new SaveObj();
                obj.Player = Player.Current;
                obj.ShareViews = VideoViewer.GetShares();
                obj.BoughtViews = VideoViewer.GetBoughtViews();
                obj.Studies = Studies.Current;
                obj.StoreItems = StoreItems.Current;

                foreach (var rival in Rivals.Current.All)
                {
                    rival.Channel.SubscribersOverTime.Clear();
                    rival.Channel.IncomeOverTime.Clear();
                    rival.Channel.ExpensesOverTime.Clear();

                    foreach (var video in rival.Channel.Videos)
                    {
                        video.Comments.Clear();
                    }
                }
                obj.Rivals = Rivals.Current.All;

                var xml = SerializationHelpers.ToXml(obj);

                File.WriteAllText(fileName, xml);
            }
            catch (Exception)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.Failure.Translate(), EnglishStrings.SaveFail.Translate(), MessagePicture.Error);
            }
        }

        public static void Load(string fileName)
        {
            try
            {
                var xml = File.ReadAllText(fileName);

                var saveObj = SerializationHelpers.FromXml<SaveObj>(xml);

                Player.Current = saveObj.Player;
                Player.Current.Channels.RemoveAt(0);
                Channel.UnreleasedVideos = Player.Current.Channels[0];

                VideoViewer.SetShares(saveObj.ShareViews);
                VideoViewer.SetBoughtViews(saveObj.BoughtViews);
                Studies.Current = saveObj.Studies;
                StoreItems.Current = saveObj.StoreItems;

                if (saveObj.Rivals.Count > 0)
                    Rivals.Current.PopulateFromList(saveObj.Rivals);
            }
            catch (Exception)
            {
                CustomMessageBox.ShowDialog(EnglishStrings.Failure.Translate(), EnglishStrings.LoadFail.Translate(), MessagePicture.Error);
            }
        }
    }
}