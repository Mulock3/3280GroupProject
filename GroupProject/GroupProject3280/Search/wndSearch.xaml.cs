using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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

namespace GroupProject3280.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// This remembers the last selection while the window is active
        /// </summary>
        public string currentSelection = "";
        /// <summary>
        /// Sets the current row length for the main table
        /// </summary>
        public int currentRowLength = 0;
        /// <summary>
        /// Gets the class for all of our search logic that we need
        /// </summary>
        private clsSearchLogic newCLSSearchLogic = new clsSearchLogic();
        /// <summary>
        /// Selected ID value; if invalid ID it is set to -1
        /// </summary>
        public int selectedInvoiceID = -1;

        /// <summary>
        /// Sets up the initial values for the Window (requires DB to have useful UI)
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();

            UpdateUI();
        }

        #region Button and Combo Boxes
        /// <summary>
        /// Opens up a new Items page from the selected Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gets selected index from datatable
                int indexSelected = DataTable.SelectedIndex;
                DataRowView invoiceIDView;

                // Makes sure everything is in range; otherwise cancels
                if (indexSelected >= 0 && indexSelected < currentRowLength)
                {
                    invoiceIDView = (DataRowView)DataTable.Items.GetItemAt(indexSelected);
                }
                else
                {
                    return;
                }

                string invoiceID = invoiceIDView.Row[0].ToString();

                selectedInvoiceID = Convert.ToInt32(invoiceID);

                // TBI Opens the items page from the specified index
                MessageBox.Show("Invoice Data for #" + invoiceID + " selected");
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Updates the main table according to the selected date, id, and charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectorIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // The folloiwng below is important in case nothing is selected
                string selectedDate;
                if (dateSelector.SelectedItem != null)
                {
                    selectedDate = dateSelector.SelectedItem.ToString().Trim();
                }
                else
                {
                    selectedDate = "";
                }

                string selectedID;
                if (invoiceIDSelector.SelectedItem != null)
                {
                    selectedID = invoiceIDSelector.SelectedItem.ToString().Trim();
                }
                else
                {
                    selectedID = "";
                }

                string selectedCharge;
                if (chargeAmountSelector.SelectedItem != null)
                {
                    selectedCharge = chargeAmountSelector.SelectedItem.ToString().Trim();
                }
                else
                {
                    selectedCharge = "";
                }

                // Updates the current selection based on selected values
                currentSelection = newCLSSearchLogic.DateIDChargeSelection(selectedDate, selectedID, selectedCharge);

                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        /// <summary>
        /// Updates the UI to make sure you have what you want
        /// </summary>
        private void UpdateUI()
        {
            try
            {
                // Gets all of our Data updated so we can use it
                newCLSSearchLogic.UpdateData(currentSelection);

                currentRowLength = newCLSSearchLogic.TableRowSizeMasterTable;

                // This will retrieve all invoice dates and populate the dateSelector list
                dateSelector.ItemsSource = newCLSSearchLogic.allInvoiceDatesList;


                // This will retrieve all invoice id's and populate the idSelector list
                invoiceIDSelector.ItemsSource = newCLSSearchLogic.allInvoiceIDsList;

                // This will retrieve all invoice charges and populate the chargeSelector list
                chargeAmountSelector.ItemsSource = newCLSSearchLogic.allChargeAmountsList;

                // This will set the datatable to represent all values in the Database according to selection
                DataTable.ItemsSource = newCLSSearchLogic.allInvoicesSelected.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
    }
}
