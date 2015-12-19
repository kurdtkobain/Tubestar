using System;
using System.Xml.Serialization;

namespace TubeStar
{
    public class Cats : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.CatsAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.CatsAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 2; }
        }

        public int QualityIncrease
        {
            get { return 10; }
        }
    }
}