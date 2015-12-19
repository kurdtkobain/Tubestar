using System;
using System.Windows.Media;

namespace TubeStar
{
    public class Lawyer : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.LawyerStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return String.Format(EnglishStrings.LawyerStoreItemDescription.Translate(), AdditionalCost.ToCurrencyString()); }
        }

        public override string ImageName
        {
            get { return "Lawyer"; }
        }

        public override int Cost
        {
            get { return 2000; }
        }

        public double AdditionalCost
        {
            get { return 500; }
        }

        public override int SkillModifier
        {
            get { return 0; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.None; }
        }
    }
}