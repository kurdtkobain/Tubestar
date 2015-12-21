namespace TubeStar
{
    public class Rival7 : Rival
    {
        public Rival7()
            : base()
        {
            Name = RivalStrings.Rival7Name.Translate();
            ShootingSkill = 45;
            PostProductionSkill = 30;
            InitialSubscribers = 1200;
            VideoKeyword1 = RivalStrings.Rival7Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival7Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival7Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival7Keyword4.Translate();
        }
    }
}