namespace TubeStar
{
    public class StudyAudienceAnalysisII : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyAudienceAnalysis2.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return Studies.Current.AudienceAnalysisI; }
        }

        public override int Cost
        {
            get { return 800; }
        }

        public override int HoursToComplete
        {
            get { return 10; }
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