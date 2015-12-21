using System.Collections.Generic;

namespace TubeStar
{
    public class StoreItems
    {
        private static StoreItems _current;

        public static StoreItems Current
        {
            get
            {
                if (_current == null)
                    _current = new StoreItems();
                return _current;
            }
            set { _current = value; }
        }

        public VideoCameraI VideoCameraI { get; set; }
        public VideoCameraII VideoCameraII { get; set; }

        public EditingSoftwareI EditingSoftwareI { get; set; }
        public EditingSoftwareII EditingSoftwareII { get; set; }

        public Lawyer Lawyer { get; set; }
        public Consultant Consultant { get; set; }

        public Loan Loan { get; set; }

        public List<StoreItem> All
        {
            get
            {
                return new List<StoreItem>()
                {
                    Loan,

                    Consultant,
                    Lawyer,

                    EditingSoftwareII,
                    EditingSoftwareI,

                    VideoCameraII,
                    VideoCameraI,
                };
            }
        }

        public StoreItems()
        {
            VideoCameraI = new VideoCameraI();
            VideoCameraII = new VideoCameraII();

            EditingSoftwareI = new EditingSoftwareI();
            EditingSoftwareII = new EditingSoftwareII();

            Lawyer = new Lawyer();
            Consultant = new Consultant();

            Loan = new Loan();
        }
    }
}