namespace TubeStar
{
    public class StudyPostProductionIII : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyPostProduction3.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return Studies.Current.PostProductionII; }
        }

        public override int Cost
        {
            get { return 600; }
        }

        public override int HoursToComplete
        {
            get { return 16; }
        }

        public override int SkillModifier
        {
            get { return 20; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.PostProduction; }
        }
    }
}