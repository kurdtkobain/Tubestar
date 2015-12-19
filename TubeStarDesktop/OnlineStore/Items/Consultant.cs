using System;
using System.Windows.Media;

namespace TubeStar
{
    public class Consultant : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.ConsultantStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return String.Format(EnglishStrings.ConsultantStoreItemDescription.Translate(), AdditionalCost.ToCurrencyString()); }
        }

        public override string ImageName
        {
            get { return "Consultant"; }
        }

        public override int Cost
        {
            get { return 3000; }
        }

        public double AdditionalCost
        {
            get { return 150; }
        }

        public override int SkillModifier
        {
            get { return 10; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.ViewQuality; }
        }
    }
}