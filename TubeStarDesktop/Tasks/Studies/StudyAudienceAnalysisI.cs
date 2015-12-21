namespace TubeStar
{
    public class StudyAudienceAnalysisI : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyAudienceAnalysis1.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return null; }
        }

        public override int Cost
        {
            get { return 800; }
        }

        public override int HoursToComplete
        {
            get { return 5; }
        }

        public override int SkillModifier
        {
            get { return 1; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.VideoAttribute; }
        }
    }
}