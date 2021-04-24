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
         *  Show and hide error message if date isn't selected for new invoices.
         *  Implement and Verify updating item list reflects changes.
         * */
        #endregion

        #region Attributes

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
        /// Control boolean to determine if an invoice is new or existing.
        /// Used when enabling and disabling certain controls.
        /// </summary>
        private bool isNewInvoice;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor that initializes the components and binds the items combo box to the items list in the main logic class
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            itemsWindow = new Items.wndItems();
            searchWindow = new Search.wndSearch();
            mainLogic = new clsMainLogic();
            isNewInvoice = false;
            cboItems.ItemsSource = mainLogic.Items;
            dgInvoiceItems.ItemsSource = mainLogic.InvoiceItems;
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
            searchWindow.UpdateUI();
            searchWindow.ShowDialog(); // If an invoice was selected, this class has the ability to access and modify the current selectedID.
            this.Show();
            if (searchWindow.selectedInvoiceID != -1)
            {
                mainLogic.SetInvoice(searchWindow.selectedInvoiceID);
                lblInvoiceNumber.Content = "Invoice # : " + mainLogic.GetInvoiceNumber();
                mainLogic.PopulateInvoiceItems();
                lblTotal.Content = "Total: $" + mainLogic.currentTotalCost;
                dpInvoiceDate.SelectedDate = mainLogic.GetInvoiceDate();
                miEdit.IsEnabled = true;
                miDelete.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No Invoice ID Selected");
            }

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
            UnlockEditOptions();
            lblInvoiceNumber.Content = "Invoice # : TBD";
            lblTotal.Content = "Total: $0";
            dpInvoiceDate.IsEnabled = true;
            isNewInvoice = true;
            mainLogic.ClearInvoiceItems();
        }

        /// <summary>
        /// When the Edit menu item is clicked, allow the user to edit the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceEditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            UnlockEditOptions();
        }

        /// <summary>
        /// When the Delete menu item is clicked, verify the user wants to delete the current invoice and then delete the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDeletMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainLogic.GetInvoiceNumber() != -1)
            {
                if (MessageBox.Show("Are you sure you would like to delete this invoice?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                {
                    mainLogic.DeleteInvoice();
                    miEdit.IsEnabled = false;
                    miDelete.IsEnabled = false;
                    lblInvoiceNumber.Content = "Invoice # :";
                    searchWindow.selectedInvoiceID = -1;
                }
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

        /// <summary>
        /// When the Items combobox has its selection changed, update the cost label to reflect the newly selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblCost.Content = "Cost: $" + mainLogic.GetItemCost(((ItemDesc)cboItems.SelectedItem).ItemCode);
        }

        #region Buttons

        /// <summary>
        /// When the remove selected items button is clicked, verify that the user wants to remove any selected items.
        /// Upon successful verification, delete any selected line items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected line items? This action cannot be undone.",
                "Delete Line Items", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                mainLogic.DeleteInvoiceItems();
                lblTotal.Content = "Total: $" + mainLogic.currentTotalCost;
            }     
        }

        /// <summary>
        /// When the add item button is clicked, add the currently selected item to the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            mainLogic.AddInvoiceItem(((ItemDesc)cboItems.SelectedItem));
            lblTotal.Content = "Total: $" + mainLogic.currentTotalCost;
        }

        /// <summary>
        /// When the save button is clicked, apply changes to the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isNewInvoice)
            {
                mainLogic.SaveInvoice(true);
                isNewInvoice = false;
                lblInvoiceNumber.Content = "Invoice # : " + mainLogic.GetInvoiceNumber();
            }
            else
            {
                mainLogic.SaveInvoice(false);
            }
            
            LockEditOptions();
        }

        /// <summary>
        /// When the cancel button is clicked, perform actions to return the screen and logic to its previous state.
        /// This options change based on whether the invoice was new or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            LockEditOptions();

            if (isNewInvoice)
            {
                isNewInvoice = false;
                
                if (mainLogic.GetInvoiceNumber() != -1)
                {
                    lblInvoiceNumber.Content = "Invoice # : " + mainLogic.GetInvoiceNumber();
                    lblTotal.Content = "Total: $" + mainLogic.GetInvoiceTotal();
                    dpInvoiceDate.SelectedDate = mainLogic.GetInvoiceDate();
                }
                else
                {
                    lblInvoiceNumber.Content = "Invoice # : ";
                    lblTotal.Content = "Total: ";
                    dpInvoiceDate.SelectedDate = DateTime.Today;
                    miEdit.IsEnabled = false;
                    miDelete.IsEnabled = false;
                } 
            }
            mainLogic.CancelSave();
        }

        #endregion

        /// <summary>
        /// Disables all of the buttons and input components that shouldn't be used when an invoice is not being edited.
        /// </summary>
        private void LockEditOptions()
        {
            dpInvoiceDate.IsEnabled = false;
            dgInvoiceItems.IsEnabled = false;
            cboItems.IsEnabled = false;
            btnAdd.IsEnabled = false;
            btnRemove.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            miSearch.IsEnabled = true;
            miNew.IsEnabled = true;
            miEdit.IsEnabled = true;
            miDelete.IsEnabled = true;
            miUpdate.IsEnabled = true;
        }

        /// <summary>
        /// Enables all of the buttons and input components that should be used when an invoice is being edited.
        /// </summary>
        private void UnlockEditOptions()
        {
            dgInvoiceItems.IsEnabled = true;
            cboItems.IsEnabled = true;
            btnAdd.IsEnabled = true;
            btnRemove.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            miSearch.IsEnabled = false;
            miNew.IsEnabled = false;
            miEdit.IsEnabled = false;
            miDelete.IsEnabled = false;
            miUpdate.IsEnabled = false;
        }

        /// <summary>
        /// When the invoice date picker is updated, update the current invoice date in the main logic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpInvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            mainLogic.currentInvoiceDate = (DateTime) dpInvoiceDate.SelectedDate;
        }

        #endregion
    }
}
