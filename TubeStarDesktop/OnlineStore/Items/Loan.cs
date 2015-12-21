using System;

namespace TubeStar
{
    public class Loan : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.LoanStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return String.Format(EnglishStrings.LoanStoreItemDescription.Translate(), Payout.ToCurrencyString(), AdditionalCost.ToCurrencyString(), Interest); }
        }

        public override string ImageName
        {
            get { return "Money"; }
        }

        public override int Cost
        {
            get { return 0; }
        }

        public double AdditionalCost
        {
            get { return 50; }
        }

        public int Payout
        {
            get { return 2000; }
        }

        public int Interest
        {
            get { return 50; }
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