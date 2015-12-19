using System;

namespace TubeStar
{
    public class SecondTime : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.SecondTimeAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.SecondTimeAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 3; }
        }
    }
}