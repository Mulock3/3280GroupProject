using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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

            try
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
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the Update menu item is clicked, open the item update window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsUpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                itemsWindow.ShowDialog();
                UpdateItemList();
                this.Show();
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the New menu item is clicked, allow the user to enter information to create a new invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnlockEditOptions();
                lblInvoiceNumber.Content = "Invoice # : TBD";
                lblTotal.Content = "Total: $0";
                dpInvoiceDate.IsEnabled = true;
                isNewInvoice = true;
                mainLogic.ClearInvoiceItems();
                mainLogic.UpdateTotalCost();
                dpInvoiceDate.SelectedDate = null;
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the Edit menu item is clicked, allow the user to edit the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceEditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnlockEditOptions();
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the Delete menu item is clicked, verify the user wants to delete the current invoice and then delete the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDeletMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
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
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// When the main window is closed, exit the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Used to update the list of items in the logic class after the update item window is closed.
        /// </summary>
        private void UpdateItemList()
        {
            try
            {
                mainLogic.PopulateItemsList();
                mainLogic.PopulateInvoiceItems();
                lblTotal.Content = "Total: $" + mainLogic.currentTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When the Items combobox has its selection changed, update the cost label to reflect the newly selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboItems.SelectedItem is null)
                    lblCost.Content = "Cost:";
                else
                    lblCost.Content = "Cost: $" + mainLogic.GetItemCost(((ItemDesc)cboItems.SelectedItem).ItemCode);
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
            try
            {
                if (MessageBox.Show("Are you sure you want to delete the selected line items? This action cannot be undone.",
                "Delete Line Items", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    mainLogic.DeleteInvoiceItems();
                    lblTotal.Content = "Total: $" + mainLogic.currentTotalCost;
                }
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
             
        }

        /// <summary>
        /// When the add item button is clicked, add the currently selected item to the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainLogic.AddInvoiceItem(((ItemDesc)cboItems.SelectedItem));
                lblTotal.Content = "Total: $" + mainLogic.currentTotalCost;
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the save button is clicked, apply changes to the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpInvoiceDate.SelectedDate is null)
                    lblInvoiceDateError.Visibility = Visibility.Visible;
                else
                {
                    if (isNewInvoice)
                    {
                        mainLogic.SaveInvoice(true);
                        isNewInvoice = false;
                        lblInvoiceNumber.Content = "Invoice # : " + mainLogic.GetInvoiceNumber();
                        searchWindow.selectedInvoiceID = mainLogic.GetInvoiceNumber();
                    }
                    else
                    {
                        mainLogic.SaveInvoice(false);
                    }

                    LockEditOptions();
                    lblInvoiceDateError.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the cancel button is clicked, perform actions to return the screen and logic to its previous state.
        /// This options change based on whether the invoice was new or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LockEditOptions();
                lblInvoiceDateError.Visibility = Visibility.Hidden;

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
                        miEdit.IsEnabled = false;
                        miDelete.IsEnabled = false;
                    }
                }
                mainLogic.CancelSave();
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// Disables all of the buttons and input components that shouldn't be used when an invoice is not being edited.
        /// </summary>
        private void LockEditOptions()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Enables all of the buttons and input components that should be used when an invoice is being edited.
        /// </summary>
        private void UnlockEditOptions()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When the invoice date picker is updated, update the current invoice date in the main logic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpInvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dpInvoiceDate.SelectedDate is null)
                {

                }
                else
                    mainLogic.currentInvoiceDate = (DateTime)dpInvoiceDate.SelectedDate;
            }
            catch (Exception ex)
            {
                ClsErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// Static class that is used to handle errors.
        /// This class provides error handling so each class doesn't need its own error handling methods.
        /// </summary>
        public static class ClsErrorHandler
        {
            public static void HandleError(string sClass, string sMethod, string sMessage)
            {
                try
                {
                    MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
                }
                catch (Exception ex)
                {
                    string sPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\error.txt"));
                    System.IO.File.AppendAllText(sPath, Environment.NewLine + "HandleError Exception: " + ex.Message);
                }
            }
        }
    }
}
