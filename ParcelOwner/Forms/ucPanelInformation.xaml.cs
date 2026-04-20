using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ParcelOwner.Forms
{
    /// <summary>
    /// Interaction logic for ucPanelInformation.xaml
    /// </summary>
    /// 

    public partial class ucPanelInformation : UserControl, INotifyPropertyChanged
    {
        // * * * * * * * * * * * * * * * * EVENTS

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = "") => PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));

        // * * * * * * * * * * * * * * * * * Properties

        public ObservableCollection<int> ListOfAddedParcels { get; set; } = new ObservableCollection<int>();

        public string FootagePrice
        {  
            get => AutoCADCommands.PricePerM2.ToString("0.00");
            set
            {
                if(double.TryParse(value, out double result))
                {
                    AutoCADCommands.PricePerM2 = result;
                }
            }
        }
        private string m_Information = string.Empty;
        public string Information
        {
            get => m_Information;
            set
            {
                m_Information = value;
                OnPropertyChanged();
            }
        }

        public ucPanelInformation()
        {
            InitializeComponent();
        }

        private void TxtFootagePrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string validChars = "0123456789.";

            if (validChars.Contains(e.Text) == false)
            {
                e.Handled = true;
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptStatus ps = PromptStatus.OK;

            while (ps == PromptStatus.OK)
            {
                PromptEntityResult per = ed.GetEntity("\nSelect polyline");
                ps = per.Status;
                if (per.ObjectId != ObjectId.Null)
                {
                    bool selectedBefore = false;
                    int parcelnr = 0;

                    foreach (Classes.ParcelObject po in AutoCADCommands.AllParcels) // <<== page 184 has Parcels instead of Classes
                    {
                        if (po.ID == per.ObjectId)
                        {
                            selectedBefore = true;
                            parcelnr = po.Number;
                            break;
                        }
                    }
                    if (selectedBefore == false)
                    {
                        // Cast object and read properties
                        using (Transaction tr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
                        {
                            Entity ent = tr.GetObject(per.ObjectId, OpenMode.ForRead) as Entity;
                            if (ent is Autodesk.AutoCAD.DatabaseServices.Polyline)
                            {
                                // Add new ParcelObject to list
                                Classes.ParcelObject po = new Classes.ParcelObject(per.ObjectId, AutoCADCommands.ParcelNextNumber); // <<== page 184 has Parcels instead of Classes
                                po.IsSold = 0;
                                AutoCADCommands.AllParcels.Add(po);

                                // Also add to the ListBox  <<== per page 184
                                //lbParcels.Items.Add(po.Number.ToString()); // <<== per page 184 but lbParcels does not exist

                                // Add number to the list <<== this was no longer on page 184 but keeping since above doesn't work
                                ListOfAddedParcels.Add(po.Number); // <<== this was no longer on page 184 but keeping since above doesn't work

                                // Increase sequence number
                                AutoCADCommands.ParcelNextNumber++;

                                // Tell the user what just happened
                                ed.WriteMessage("\nParcel " + po.Number.ToString() + " added.");
                            }
                            tr.Commit();
                        }
                    }
                    else
                    {
                        ed.WriteMessage("\nThis polyline was selected before as parcel " + parcelnr.ToString());
                    }
                }
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            // TODO: complete function
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            // TODO: complete function
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;

            if (lb != null && lb.SelectedItem != null)
            {
                int parcelNumber = (int)lb.SelectedItem;

                foreach (Classes.ParcelObject po in AutoCADCommands.AllParcels)
                {
                    if (po.Number == parcelNumber)
                    {
                        // We've got it!!
                        if (po.IsSold == 0)
                        {
                            Information = "Parcel with an area of " + po.Area.ToString("F2") + "m² is for sale for " + po.TotalPriceAsstring;
                        }
                        else
                        {
                            Information = "Parcel is sold to " + po.Name;
                        }

                        break;
                    }
                }
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = sender as ListBox;

            if (lb != null && lb.SelectedItem != null )
            {
                int parcelNumber = (int)lb.SelectedItem;

                foreach (Classes.ParcelObject po in AutoCADCommands.AllParcels)
                {
                    if (po.Number == parcelNumber)
                    {
                        // We've got it!
                        ParcelProperties form = new ParcelProperties(po);
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowModalWindow(form);

                        break;
                    }
                }
            }
        }
    }
}
