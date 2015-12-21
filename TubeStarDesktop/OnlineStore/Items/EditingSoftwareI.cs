namespace TubeStar
{
    public class EditingSoftwareI : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.EditingSoftwareIStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.EditingSoftwareIStoreItemDescription.Translate(); }
        }

        public override string ImageName
        {
            get { return "Software1"; }
        }

        public override int Cost
        {
            get { return 500; }
        }

        public override int SkillModifier
        {
            get { return 4; }
        }

        public override SkillModifierType SkillModifierType
        {
            get { return TubeStar.SkillModifierType.PostProduction; }
        }
    }
}