using System;
using System.Xml.Serialization;

namespace TubeStar
{
    public class Crowdfunding : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.CrowdFundingAttribute.Translate(); }
        }

        public override string Description
        {
            get { return String.Format(EnglishStrings.CrowdFundingAttributeDescription.Translate(), Money.ToCurrencyString()); }
        }

        public override int Cost
        {
            get { return 3; }
        }

        public double Money
        {
            get { return Player.Current.ChallengeMode ? 0.5 : 1; }
        }
    }
}