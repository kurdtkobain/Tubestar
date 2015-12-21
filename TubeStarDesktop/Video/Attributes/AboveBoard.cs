namespace TubeStar
{
    public class AboveBoard : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.AboveBoardAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.AboveBoardAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 2; }
        }
    }
}