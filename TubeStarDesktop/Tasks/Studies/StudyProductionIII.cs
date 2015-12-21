namespace TubeStar
{
    public class StudyProductionIII : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyProduction3.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return Studies.Current.ProductionII; }
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
            get { return TubeStar.SkillModifierType.Shooting; }
        }
    }
}