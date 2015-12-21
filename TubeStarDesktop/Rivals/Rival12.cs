namespace TubeStar
{
    public class Rival12 : Rival
    {
        public Rival12()
            : base()
        {
            Name = RivalStrings.Rival12Name.Translate();
            ShootingSkill = 55;
            PostProductionSkill = 45;
            InitialSubscribers = 45000;
            VideoKeyword1 = RivalStrings.Rival12Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival12Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival12Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival12Keyword4.Translate();
        }
    }
}