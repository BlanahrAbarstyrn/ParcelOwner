using System.Windows;


namespace ParcelOwner.Forms
{
    /// <summary>
    /// Interaction logic for ParcelProperties.xaml
    /// </summary>
    public partial class ParcelProperties : Window
    {
        // * * * * * * * * * * * * * Variables
        private Classes.ParcelObjectViewItem m_CurrentParcel = null;
         

        // * * * * * * * * * * * * * Constructor
        public ParcelProperties(Classes.ParcelObject po)
        {
            InitializeComponent();

            //m_CurrentParcel = po;
            m_CurrentParcel = new Classes.ParcelObjectViewItem(po);

            DataContext = m_CurrentParcel;

        }


        // * * * * * * * * * * * * * Events

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            m_CurrentParcel.UpdateOwner();

            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
