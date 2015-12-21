namespace TubeStar
{
    public class EditingSoftwareII : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.EditingSoftwareIIStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.EditingSoftwareIIStoreItemDescription.Translate(); }
        }

        public override string ImageName
        {
            get { return "Software2"; }
        }

        public override int Cost
        {
            get { return 1500; }
        }

        public override int SkillModifier
        {
            get { return 10; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.PostProduction; }
        }
    }
}