namespace TubeStar
{
    public class VideoCameraI : StoreItem
    {
        public override string Name
        {
            get { return EnglishStrings.VideoCameraIStoreItem.Translate(); }
        }

        public override string Description
        {
            get { return EnglishStrings.VideoCameraIStoreItemDescription.Translate(); }
        }

        public override string ImageName
        {
            get { return "Camera1"; }
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
            get { return TubeStar.SkillModifierType.Shooting; }
        }
    }
}