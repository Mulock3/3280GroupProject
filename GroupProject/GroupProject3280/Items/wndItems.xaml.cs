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

        private clsItemsLogic Logic;    

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

        private void wndItemsWindow_Loaded(object sender, RoutedEventArgs e) {

            GroupProject3280.InvoiceDataSet invoiceDataSet = ((GroupProject3280.InvoiceDataSet)(this.FindResource("invoiceDataSet")));
            // Load data into the table ItemDesc. You can modify this code as needed.
            GroupProject3280.InvoiceDataSetTableAdapters.ItemDescTableAdapter invoiceDataSetItemDescTableAdapter = new GroupProject3280.InvoiceDataSetTableAdapters.ItemDescTableAdapter();
            invoiceDataSetItemDescTableAdapter.Fill(invoiceDataSet.ItemDesc);
            System.Windows.Data.CollectionViewSource itemDescViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("itemDescViewSource")));
            itemDescViewSource.View.MoveCurrentToFirst();
            // load combo boxes
            cbDeleteItem.ItemsSource = Logic.GetItemDesc();
        }

        private void cbDeleteItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bDeleteItem.IsEnabled = true;
            // TODO delete the item
        }

        private void cbEditItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            SetEditEnabled(true);
        }

        private void bEditItemSave_Click(object sender, RoutedEventArgs e) {
            // TODO validate information and save to database

            // Reset the Edit Item section
            cbEditItem.SelectedIndex = -1;
            cbEditItem.SelectedItem = null;
            tbEditItemCode.Text = "";
            tbEditItemDesc.Text = "";
            tbEditItemCost.Text = "";
            SetEditEnabled(false);
        }

        private void bDeleteItem_Click(object sender, RoutedEventArgs e) {
            // TODO delete the selected item from the database
            // Reset the Delete Item section
            cbDeleteItem.SelectedIndex = -1;
            cbDeleteItem.SelectedItem = null;
            bDeleteItem.IsEnabled = false;
        }

        private void bAddItem_Click(object sender, RoutedEventArgs e) {
            // TODO validate user-entered data

            // TODO call Logic.AddItemDesc(pItemCode, pItemDesc, pCost)

            // Reset the Add Item section
            tbAddItemCode.Text = "";
            tbAddItemDesc.Text = "";
            tbAddItemCost.Text = "";
        }
    }
}
