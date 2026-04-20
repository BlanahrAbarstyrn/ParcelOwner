using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace ParcelOwner.Classes
{
    public class ParcelObject
    {
        // * * * * * * * * * * * * * * * Variables
        public ObjectId ID { get; private set; } = ObjectId.Null;
        public int Number { get; private set; } = 0;
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates the status: 0 = for sale, 1 = sold
        /// </summary>
        public int IsSold { get; set; } = 0;
        public double Area
        {
            get { return GetPolylineArea(); }
        }
        public double TotalPrice
        {
            get { return Area * AutoCADCommands.PricePerM2; }
        }
        public string TotalPriceAsstring
        {
            get { return "$ " + TotalPrice.ToString("0.00"); }
        }

        // * * * * * * * * * * * * * * * Constructor
        /// <summary>
        /// Create a new Parcel Object
        /// </summary>
        /// <param name="id">ObjectId of Polyline</param>
        /// <param name="number">Unique number</param>
        public ParcelObject(ObjectId id, int number)
        {
            ID = id;
            Number = number;
        }

        // * * * * * * * * * * * * * * * Functions
        public string Export() // TODO: finish so it doesn't return an empty string
        {
            return string.Empty;
        }

        private double GetPolylineArea()
        {
            double returnValue = 0.0;
            using (Transaction tr = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            {
                Entity ent = tr.GetObject(ID, OpenMode.ForRead) as Entity;
                if (ent is Polyline)
                {
                    Polyline pl = ent as Polyline;
                    returnValue = pl.Area;
                }
                tr.Commit();
            }
            return returnValue;
        }
    }
}
