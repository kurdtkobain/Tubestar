using System;
using System.Windows.Media;

namespace TubeStar
{
    public class StudyProductionII : Study
    {
        public override string Name
        {
            get { return EnglishStrings.StudyProduction2.Translate(); }
        }

        public override Study Prerequisite
        {
            get { return Studies.Current.ProductionI; }
        }

        public override int Cost
        {
            get { return 400; }
        }

        public override int HoursToComplete
        {
            get { return 8; }
        }

        public override int SkillModifier
        {
            get { return 10; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.Shooting; }
        }
    }
}