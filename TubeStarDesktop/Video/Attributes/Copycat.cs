using System;

namespace TubeStar
{
    public class Copycat : VideoAttribute
    {
        public override string Name
        {
            get { return EnglishStrings.CopycatAttribute.Translate(); }
        }

        public override string Description
        {
            get { return String.Format(EnglishStrings.CopycatAttributeDescription.Translate(), FreeViews); }
        }

        public override int Cost
        {
            get { return 1; }
        }

        public int FreeViews
        {
            get { return 125; }
        }
    }
}