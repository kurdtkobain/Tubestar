namespace TubeStar
{
    public class VideoCameraII : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.VideoCameraIIStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.VideoCameraIIStoreItemDescription.Translate(); }
        }

        public override string ImageName
        {
            get { return "Camera2"; }
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
            get { return TubeStar.SkillModifierType.Shooting; }
        }
    }
}