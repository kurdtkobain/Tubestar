namespace TubeStar
{
    public class ProductPlacement : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.ProductPlacementAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.ProductPlacementAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 2; }
        }
    }
}