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
            bAddItem.IsEnabled = false;
            bDeleteItem.IsEnabled = false;
            SetEditEnabled(false);
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
        private void LoadComboBoxes() {
            cbDeleteItem.ItemsSource = Logic.GetItemDesc();
            cbEditItem.ItemsSource = Logic.GetItemDesc();
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
                GroupProject3280.InvoiceDataSet invoiceDataSet = ((GroupProject3280.InvoiceDataSet)(this.FindResource("invoiceDataSet")));
                // Load data into the table ItemDesc. You can modify this code as needed.
                GroupProject3280.InvoiceDataSetTableAdapters.ItemDescTableAdapter invoiceDataSetItemDescTableAdapter = new GroupProject3280.InvoiceDataSetTableAdapters.ItemDescTableAdapter();
                invoiceDataSetItemDescTableAdapter.Fill(invoiceDataSet.ItemDesc);
                System.Windows.Data.CollectionViewSource itemDescViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("itemDescViewSource")));
                itemDescViewSource.View.MoveCurrentToFirst();
                // load combo boxes
                LoadComboBoxes();
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
                // TODO validate information and save to database

                // Reset the Edit Item section
                cbEditItem.SelectedIndex = -1;
                cbEditItem.SelectedItem = null;
                tbEditItemCode.Text = "";
                tbEditItemDesc.Text = "";
                tbEditItemCost.Text = "";
                SetEditEnabled(false);
                // Update combo boxes
                LoadComboBoxes();
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
                // delete the selected item from the database
                if (cbDeleteItem.SelectedItem is ItemDesc) {
                    Logic.DeleteItem(((ItemDesc)cbDeleteItem.SelectedItem).ItemCode);
                }
                // Reset the Delete Item section
                cbDeleteItem.SelectedIndex = -1;
                cbDeleteItem.SelectedItem = null;
                bDeleteItem.IsEnabled = false;
                // Update combo boxes
                LoadComboBoxes();
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
                // hide error labels
                lAddItemCodeErr.Visibility = Visibility.Hidden;
                lAddItemDescErr.Visibility = Visibility.Hidden;
                lAddItemCostErr.Visibility = Visibility.Hidden;
                // get the values of user-entered data
                string itemCode = tbAddItemCode.Text;
                string itemDesc = tbAddItemDesc.Text;
                string itemCost = tbAddItemCost.Text;
                decimal dItemCost = -1;
                // validate item code
                if (!Logic.ValidateAddCode(itemCode)) {
                    lAddItemCodeErr.Visibility = Visibility.Visible;
                    return;
                }
                // validate item desc
                if (!Logic.ValidateAddDesc(itemDesc)) {
                    lAddItemDescErr.Visibility = Visibility.Visible;
                    return;
                }
                // validate item cost
                if (!Logic.ValidateAddCost(itemCost, out dItemCost)) {
                    lAddItemCostErr.Visibility = Visibility.Visible;
                    return;
                }
                // Add the data
                Logic.AddItemDesc(itemCode, itemDesc, dItemCost);
                // Reset the Add Item section
                tbAddItemCode.Text = "";
                tbAddItemDesc.Text = "";
                tbAddItemCost.Text = "";
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        #endregion

        #endregion
    }
}
