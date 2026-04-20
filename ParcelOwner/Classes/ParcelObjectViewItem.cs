using System.Collections.Generic;


namespace ParcelOwner.Classes
{
    class ParcelObjectViewItem
    {
        // * * * * * * * * * * * * * * Properties
        public ParcelObject OriginalParcel = null;

        public Dictionary<int, string> SoldValues
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    { 0, "For Sale" },
                    { 1, "Already Sold" }
                };
            }
        }

        public string Number { get; }

        public int Sold { get; set; }
        public string OwnerName { get; set; }
        public string Price { get; }
        public string Area { get; }

        // * * * * * * * * * * * * * * Constructor
        public ParcelObjectViewItem(ParcelObject po)
        {
            OriginalParcel = po;
            Number = po.Number.ToString();
            Sold = po.IsSold;
            OwnerName = po.Name;
            Price = po.TotalPriceAsstring;
            Area = po.Area.ToString("0.0m²");
        }

        // * * * * * * * * * * * * * * Functions
        public void UpdateOwner()
        {
            OriginalParcel.IsSold = Sold;
            if (Sold != 0) { OriginalParcel.Name = OwnerName; }
        }
    }
}
