using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Serialization;

namespace TubeStar
{
    [XmlInclude(typeof(VideoCameraI))]
    [XmlInclude(typeof(VideoCameraII))]
    [XmlInclude(typeof(EditingSoftwareI))]
    [XmlInclude(typeof(EditingSoftwareII))]
    [XmlInclude(typeof(Lawyer))]
    [XmlInclude(typeof(Consultant))]
    [XmlInclude(typeof(Loan))]
    public abstract class StoreItem
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Cost { get; }
        public abstract string ImageName { get; }

        public abstract int SkillModifier { get; }
        public abstract SkillModifierType SkillModifierType { get; }

        public bool Purchased { get; set; }
    }
}