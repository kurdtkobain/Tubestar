namespace TubeStar
{
    public class Memetic : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.MemeticAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.MemeticAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 1; }
        }
    }
}