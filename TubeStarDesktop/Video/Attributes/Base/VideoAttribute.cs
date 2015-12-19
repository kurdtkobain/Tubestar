using System;
using System.Xml.Serialization;

namespace TubeStar
{
    [XmlInclude(typeof(Cats))]
    [XmlInclude(typeof(Copycat))]
    [XmlInclude(typeof(FanboyBait))]
    [XmlInclude(typeof(GenreBuster))]
    [XmlInclude(typeof(Hypnotic))]
    [XmlInclude(typeof(LearnFromMistakes))]
    [XmlInclude(typeof(Memetic))]
    [XmlInclude(typeof(ProductPlacement))]
    [XmlInclude(typeof(SecondTime))]
    [XmlInclude(typeof(SoBad))]
    [XmlInclude(typeof(Crowdfunding))]
    [XmlInclude(typeof(AboveBoard))]
    public abstract class VideoAttribute
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Cost { get; }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;

            var other = obj as VideoAttribute;
            if (other != null && other.Name == this.Name) return true;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}