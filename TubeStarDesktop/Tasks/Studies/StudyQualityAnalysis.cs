namespace TubeStar
{
    public class StudyQualityAnalysis : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyQualityAnalysis.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return null; }
        }

        public override int Cost
        {
            get { return 400; }
        }

        public override int HoursToComplete
        {
            get { return 10; }
        }

        public override int SkillModifier
        {
            get { return 0; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.ViewQuality; }
        }
    }
}