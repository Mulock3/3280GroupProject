using System;
using System.Collections.Generic;
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

namespace GroupProject3280.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        #region TODO
        /*
         *  Add try catch blocks to methods
         *  Provide methods and logic to disable fields from being clicked or edited when they shouldn't be.
         *  Add line item display functionality for the invoice
         *  Add ability to add/remove line items in an invoice
         * */
        #endregion

        #region Attributes

        /// <summary>
        /// ID number for the currently selected invoice.
        /// </summary>
        public int selectedID = -1;

        /// <summary>
        /// Window that will be used to update the items in the database.
        /// </summary>
        private readonly Items.wndItems itemsWindow;

        /// <summary>
        /// Window that will be used to search for an invoice.
        /// </summary>
        private readonly Search.wndSearch searchWindow;

        /// <summary>
        /// Main logic class that will handle back end logic for the main window.
        /// </summary>
        private clsMainLogic mainLogic;

        /// <summary>
        /// Control boolean to determine if an invoice is currently being edited.
        /// Used when enabling and disabling certain controls.
        /// </summary>
        private bool isInvoiceBeingEdited;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor that initializes the components and binds the items combo box to the items list in the main logic class
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            itemsWindow = new Items.wndItems();
            searchWindow = new Search.wndSearch(this);
            mainLogic = new clsMainLogic();
            isInvoiceBeingEdited = false;
            cboItems.ItemsSource = mainLogic.Items;
        }

        #region Menu Items

        /// <summary>
        /// When the Search menu item is clicked, open the invoice search window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceSearchMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            searchWindow.ShowDialog(); // If an invoice was selected, this class has the ability to access and modify the current selectedID.
            this.Show();
        }

        /// <summary>
        /// When the Update menu item is clicked, open the item update window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsUpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            itemsWindow.ShowDialog();
            UpdateItemList();
            this.Show();
        }

        /// <summary>
        /// When the New menu item is clicked, allow the user to enter information to create a new invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO Enable editing of a new invoice and disable search, new, edit, delete options until new invoice is confirmed or canceled.
        }

        /// <summary>
        /// When the Edit menu item is clicked, allow the user to edit the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceEditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO As long as there is a current valid invoice, edit the current invoice. Disable search, new, edit, delete options until new invoice is confirmed or canceled.
        }

        /// <summary>
        /// When the Delete menu item is clicked, verify the user wants to delete the current invoice and then delete the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDeletMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (selectedID != -1)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you would like to delete this invoice?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            }
               
            //TODO when a user says yes, delete the current invoice.
            //if (messageBoxResult == MessageBoxResult.Yes)
        }

        #endregion

        /// <summary>
        /// When the main window is closed, exit the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Used to update the list of items in the logic class after the update item window is closed.
        /// </summary>
        private void UpdateItemList()
        {
            // TODO provide logic in the main logic class that udpates the item list after the update item window is closed.
        }
        #endregion
    }
}
