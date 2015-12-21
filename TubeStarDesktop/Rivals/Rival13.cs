namespace TubeStar
{
    public class Rival13 : Rival
    {
        public Rival13()
            : base()
        {
            Name = RivalStrings.Rival13Name.Translate();
            ShootingSkill = 60;
            PostProductionSkill = 45;
            InitialSubscribers = 60000;
            VideoKeyword1 = RivalStrings.Rival13Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival13Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival13Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival13Keyword4.Translate();
        }
    }
}