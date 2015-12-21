namespace TubeStar
{
    public class StudyProductionI : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyProduction1.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return null; }
        }

        public override int Cost
        {
            get { return 200; }
        }

        public override int HoursToComplete
        {
            get { return 4; }
        }

        public override int SkillModifier
        {
            get { return 5; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.Shooting; }
        }
    }
}