namespace TubeStar
{
    public class Hypnotic : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.HypnoticAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.HypnoticAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 3; }
        }
    }
}