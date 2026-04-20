using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;

namespace ParcelOwner.Forms
{
    /// <summary>
    /// Interaction logic for ParcelProperties.xaml
    /// </summary>
    public partial class ParcelProperties : Window, INotifyPropertyChanged
    {
        // * * * * * * * * * * * * * Variables
        private Classes.ParcelObject m_CurrentParcel = null;

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

            

        // * * * * * * * * * * * * * Constructor
        public ParcelProperties(Classes.ParcelObject po)
        {
            InitializeComponent();

            m_CurrentParcel = po;

            Number = po.Number.ToString();
            Sold = po.IsSold;
            OwnerName = po.Name;
            Price = po.TotalPriceAsstring;
            Area = po.Area.ToString("0.0m²");

            OnPropertyChanged("Number");
            OnPropertyChanged("Sold");
            OnPropertyChanged("OwnerName");
            OnPropertyChanged("Price");
            OnPropertyChanged("Area");
        }


        // * * * * * * * * * * * * * Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            m_CurrentParcel.IsSold = Sold;
            if (m_CurrentParcel.IsSold != 0)
            {
                m_CurrentParcel.Name = OwnerName;
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void OK(object sender, RoutedEventArgs e)
        {

        }
    }
}
