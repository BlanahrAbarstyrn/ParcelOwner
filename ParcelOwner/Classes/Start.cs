
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;


namespace ParcelOwner
{
    public class AutoCADCommands
    {
        public static PaletteSet ps = null;
        public static double PricePerM2 = 100.0;
        public static int ParcelNextNumber = 1;
        public static List<Classes.ParcelObject> AllParcels = new List<Classes.ParcelObject>();

        [CommandMethod("Parcels")]
        public void cmdParcels()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (ps == null)
                {
                    ps = new PaletteSet("Ownership", "", new Guid("{8981761B-A1AF-4712-AE7C-6372058A57E0}"));
                    
                    // Line of code from previous section book saide to change
                    // but ended up keeping for now
                    ps.AddVisual("Overview", new Forms.ucPanelInformation());

                    // This is the line of code the book said to use but did not work
                    // Had to keep line as it was written in previous section
                    //ps.Add("Overview", new PE.Forms.ucPanelInformation());
                }
                ps.Visible = true;
                ps.MinimumSize = new System.Drawing.Size(300, 600);
            } //try
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nError: " + ex.Message);
            } //catch
        } //void
    } //class
} //namespace
