using System;

namespace TubeStar
{
    public abstract class UniqueObject
    {
        public Guid Id { get; set; }

        public UniqueObject()
        {
            Id = Guid.NewGuid();
        }

        #region Equals, GetHashCode overrides

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;

            var other = obj as UniqueObject;
            if (other != null && other.Id == this.Id) return true;

            return base.Equals(obj);
        }

        #endregion Equals, GetHashCode overrides
    }
}