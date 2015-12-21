namespace TubeStar
{
    public class GenreBuster : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.GenreBusterAttribute.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.GenreBusterAttributeDescription.Translate(); }
        }

        public override int Cost
        {
            get { return 3; }
        }
    }
}