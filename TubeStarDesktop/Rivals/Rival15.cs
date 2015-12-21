namespace TubeStar
{
    public class Rival15 : Rival
    {
        public Rival15()
            : base()
        {
            Name = RivalStrings.Rival15Name.Translate();
            ShootingSkill = 65;
            PostProductionSkill = 55;
            InitialSubscribers = 90000;
            VideoKeyword1 = RivalStrings.Rival15Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival15Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival15Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival15Keyword4.Translate();
        }
    }
}