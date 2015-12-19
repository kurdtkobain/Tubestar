using System;

namespace TubeStar
{
    public class SoBad : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.SoBadAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.SoBadAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 2; }
        }
    }
}