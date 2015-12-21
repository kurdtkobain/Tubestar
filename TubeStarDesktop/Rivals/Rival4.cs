namespace TubeStar
{
    public class Rival4 : Rival
    {
        public Rival4()
            : base()
        {
            Name = RivalStrings.Rival4Name.Translate();
            ShootingSkill = 35;
            PostProductionSkill = 25;
            InitialSubscribers = 200;
            VideoKeyword1 = RivalStrings.Rival4Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival4Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival4Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival4Keyword4.Translate();
        }
    }
}