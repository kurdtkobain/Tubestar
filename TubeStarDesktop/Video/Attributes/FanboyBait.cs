namespace TubeStar
{
    public class FanboyBait : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.FanboyBaitAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.FanboyBaitAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 4; }
        }
    }
}