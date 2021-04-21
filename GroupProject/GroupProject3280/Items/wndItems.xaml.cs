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
using GroupProject3280.InvoiceDataSetTableAdapters;

namespace GroupProject3280.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region ATTRIBUTES

        /// <summary>Helper class that handles interaction with the database</summary>
        private clsItemsLogic Logic;

        /// <summary>
        /// This property indicates whether changes have been made
        /// to the database. It is set to false when the window is
        /// opened and set to true when items are added/removed/modified
        /// </summary>
        public bool IsChanged { get; private set; }

        #endregion

        #region METHODS

        /// <summary>wndItems constructor</summary>
        public wndItems() {
            InitializeComponent();
            Logic = new clsItemsLogic(MainWindow.Database);
            SetEditEnabled(false);
            // Hide error labels
            lAddItemCodeErr.Visibility = Visibility.Hidden;
            lAddItemCostErr.Visibility = Visibility.Hidden;
            lAddItemDescErr.Visibility = Visibility.Hidden;
            lEditItemCostErr.Visibility = Visibility.Hidden;
            lEditItemDescErr.Visibility = Visibility.Hidden;
            // Disable components
            bDeleteItem.IsEnabled = false;
            SetEditEnabled(false);
            // Clear text fields
            tbAddItemCode.Text = "";
            tbAddItemCost.Text = "";
            tbAddItemDesc.Text = "";
            tbEditItemCode.Text = "";
            tbEditItemCost.Text = "";
            tbEditItemDesc.Text = "";
        }

        /// <summary>
        /// Called when the Edit combo box is changed
        /// Enables the text boxes and buttons in the Edit section.
        /// </summary>
        /// <param name="enabled">True to enable the components</param>
        private void SetEditEnabled(bool enabled) {
            tbEditItemCost.IsEnabled = enabled;
            tbEditItemDesc.IsEnabled = enabled;
            bEditItemSave.IsEnabled = enabled;
        }

        /// <summary>
        /// Used to update combo boxes to the latest version of the database
        /// </summary>
        private void OnChangeDatabase() {
            GroupProject3280.InvoiceDataSet invoiceDataSet = ((GroupProject3280.InvoiceDataSet)(this.FindResource("invoiceDataSet")));
            // Load data into the table ItemDesc.
            GroupProject3280.InvoiceDataSetTableAdapters.ItemDescTableAdapter invoiceDataSetItemDescTableAdapter = new GroupProject3280.InvoiceDataSetTableAdapters.ItemDescTableAdapter();
            invoiceDataSetItemDescTableAdapter.Fill(invoiceDataSet.ItemDesc);
            System.Windows.Data.CollectionViewSource itemDescViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("itemDescViewSource")));
            itemDescViewSource.View.MoveCurrentToFirst();
            // Refresh item sources
            cbDeleteItem.ItemsSource = Logic.GetItemDesc();
            cbEditItem.ItemsSource = Logic.GetItemDesc();
            IsChanged = true;
        }

        #region EVENT_METHODS

        /// <summary>
        /// Called when the window is first loaded.
        /// Initializes the data grid with the database bindings.
        /// </summary>
        /// <param name="sender">The window</param>
        /// <param name="e">The event args</param>
        private void wndItemsWindow_Loaded(object sender, RoutedEventArgs e) {
            try {
                // Refresh view
                OnChangeDatabase();
                // Reset IsChanged
                IsChanged = false;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        /// <summary>
        /// Called when the Delete Item combo box changes.
        /// Enables the delete item button.
        /// </summary>
        /// <param name="sender">The combo box that was changed</param>
        /// <param name="e">The event args</param>
        private void cbDeleteItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bDeleteItem.IsEnabled = true;
        }

        /// <summary>
        /// Called when the Edit Item combo box changes.
        /// Populates textboxes and enables the rest of the Edit Item section.
        /// </summary>
        /// <param name="sender">The combo box that was changed</param>
        /// <param name="e">The event args</param>
        private void cbEditItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                SetEditEnabled(true);
                // get the selected item from the database
                if (cbEditItem.SelectedItem is ItemDesc) {
                    List<ItemDesc> list = Logic.GetItemDesc(((ItemDesc)cbEditItem.SelectedItem).ItemCode);
                    // use the first item in the list to populate textboxes
                    if (list.Count > 0) {
                        tbEditItemCode.Text = list[0].ItemCode;
                        tbEditItemDesc.Text = list[0].Desc;
                        tbEditItemCost.Text = list[0].Cost.ToString();
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        /// <summary>
        /// Called when the Edit Item save button is clicked.
        /// Validates the textboxes and updates the item desc
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event args</param>
        private void bEditItemSave_Click(object sender, RoutedEventArgs e) {
            try {
                // Hide error labels
                lEditItemDescErr.Visibility = Visibility.Hidden;
                lEditItemCostErr.Visibility = Visibility.Hidden;
                // Get the values of user-entered data
                string itemCode = tbEditItemCode.Text;
                string itemDesc = tbEditItemDesc.Text;
                string itemCost = tbEditItemCost.Text;
                decimal dItemCost = -1;
                // Validate item desc
                if (!Logic.ValidateDescString(itemDesc)) {
                    lEditItemDescErr.Visibility = Visibility.Visible;
                    return;
                }
                // Validate item cost
                if (!Logic.ValidateCostString(itemCost, out dItemCost)) {
                    lEditItemCostErr.Visibility = Visibility.Visible;
                    return;
                }
                // Edit the item in the database
                if (Logic.UpdateItemDesc(itemCode, itemDesc, dItemCost)) {
                    // Reset the Edit Item section
                    cbEditItem.SelectedIndex = -1;
                    cbEditItem.SelectedItem = null;
                    tbEditItemCode.Text = "";
                    tbEditItemDesc.Text = "";
                    tbEditItemCost.Text = "";
                    SetEditEnabled(false);
                    // Refresh view
                    OnChangeDatabase();
                    // Nofity user
                    string message = String.Format("Changed item with code '{0}' to have description '{1}' and unit cost ${2}", itemCode, itemDesc, itemCost);
                    MessageBox.Show(message, "Edit Item success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        /// <summary>
        /// Called when the Delete Item button is clicked.
        /// Deletes the selected item from the database
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event args</param>
        private void bDeleteItem_Click(object sender, RoutedEventArgs e) {
            try {
                // Delete the selected item from the database
                if (cbDeleteItem.SelectedItem is ItemDesc) {
                    ItemDesc item = (ItemDesc)cbDeleteItem.SelectedItem;
                    string desc = item.Desc;
                    string code = item.ItemCode;
                    if (Logic.DeleteItem(code)) {
                        // Reset the Delete Item section
                        cbDeleteItem.SelectedIndex = -1;
                        cbDeleteItem.SelectedItem = null;
                        bDeleteItem.IsEnabled = false;
                        // Refresh view
                        OnChangeDatabase();
                        // Notify the user
                        string message = String.Format("Deleted '{0}' ({1})", desc, code);
                        MessageBox.Show(message, "Delete Item success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            
        }

        /// <summary>
        /// Called when the Add Item button is clicked.
        /// Validates textbox entries and either shows error labels
        /// or adds the item to the database
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event args</param>
        private void bAddItem_Click(object sender, RoutedEventArgs e) {
            try {
                // Hide error labels
                lAddItemCodeErr.Visibility = Visibility.Hidden;
                lAddItemDescErr.Visibility = Visibility.Hidden;
                lAddItemCostErr.Visibility = Visibility.Hidden;
                // Get the values of user-entered data
                string itemCode = tbAddItemCode.Text;
                string itemDesc = tbAddItemDesc.Text;
                string itemCost = tbAddItemCost.Text;
                decimal dItemCost = -1;
                // Validate item code
                if (!Logic.ValidateAddCode(itemCode)) {
                    lAddItemCodeErr.Visibility = Visibility.Visible;
                    return;
                }
                // Validate item desc
                if (!Logic.ValidateDescString(itemDesc)) {
                    lAddItemDescErr.Visibility = Visibility.Visible;
                    return;
                }
                // Validate item cost
                if (!Logic.ValidateCostString(itemCost, out dItemCost)) {
                    lAddItemCostErr.Visibility = Visibility.Visible;
                    return;
                }
                // Add the data
                if (Logic.AddItemDesc(itemCode, itemDesc, dItemCost)) {
                    // Refresh view
                    OnChangeDatabase();
                    // Reset the Add Item section
                    tbAddItemCode.Text = "";
                    tbAddItemDesc.Text = "";
                    tbAddItemCost.Text = "";
                    string message = String.Format("Added '{0}' with code '{1}' and unit cost ${2}", itemDesc, itemCode, itemCost);
                    MessageBox.Show(message, "Add Item success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        /// <summary>
        /// Called when a key is typed in a cost text box.
        /// Only allows numbers and decimals
        /// </summary>
        /// <param name="sender">The textbox that is being changed</param>
        /// <param name="e">The key event args</param>
        private void tbItemCost_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                // Only allow numbers to be entered
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)) {
                    // Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Decimal)) {
                        e.Handled = true;
                    }
                }
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion

        #endregion

    }
}
