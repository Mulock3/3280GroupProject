using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Main
{
    class clsMainLogic
    {
        #region Attributes

        /// <summary>
        /// Database manager class that allows us to connect and perform queries and return information from a database.
        /// </summary>
        private readonly DatabaseManager db;

        /// <summary>
        /// Stores the SQL statement being used to make database transactions.
        /// </summary>
        private string sSQL;

        /// <summary>
        /// The return value of the SQL calls.
        /// </summary>
        private int iReturn;

        /// <summary>
        /// The dataset returned by the SQL calls.
        /// </summary>
        private DataSet dataSet;

        /// <summary>
        /// Invoice object that holds the current invoice.
        /// </summary>
        private Invoice invoice;

        /// <summary>
        /// Current total cost of the invoice. 
        /// Used when the invoice is currently being updated and has a different cost than what is shown in the database.
        /// </summary>
        public decimal currentTotalCost;

        /// <summary>
        /// Current invoice date.
        /// Used when the invoice is being updated and has a different invoice date than what is shown in the database.
        /// </summary>
        public DateTime currentInvoiceDate;

        /// <summary>
        /// List of items that will be used when adding or removing from an invoice.
        /// </summary>
        private readonly ObservableCollection<ItemDesc> items;

        /// <summary>
        /// Publicly accessible list of items that will be used when adding or removing from an invoice.
        /// </summary>
        public ObservableCollection<ItemDesc> Items { get => items; }

        /// <summary>
        /// List of items on a particular invoice. 
        /// </summary>
        private ObservableCollection<SelectableLineItem> invoiceItems;

        /// <summary>
        /// Publicly accessible list of items on a particular invoice.
        /// </summary>
        public ObservableCollection<SelectableLineItem> InvoiceItems { get => invoiceItems; private set => invoiceItems = value; }

        /// <summary>
        /// Temporary list of items on a particular invoice.
        /// Used to save the state of an invoice while it is being edited.
        /// </summary>
        private readonly ObservableCollection<SelectableLineItem> tempInvoiceItems;

        #endregion

        #region Methods

        #region Constructor
        /// <summary>
        /// Constructor that initializes the database manager and populates the current list of items.
        /// </summary>
        public clsMainLogic()
        {
            db = new DatabaseManager();
            dataSet = new DataSet();
            items = new ObservableCollection<ItemDesc>();
            invoiceItems = new ObservableCollection<SelectableLineItem>();
            tempInvoiceItems = new ObservableCollection<SelectableLineItem>();
            PopulateItemsList();
            currentTotalCost = 0m;
            currentInvoiceDate = DateTime.Today;
        }
        #endregion


        /// <summary>
        /// Runs a sql command to retreive all current items in the database and adds them to the list of items.
        /// </summary>
        public void PopulateItemsList()
        {
            sSQL = clsMainSQL.SelectItems();
            dataSet = db.ExecuteSQLStatement(sSQL, ref iReturn);

            ItemDesc item;
            Items.Clear();

            for (int i = 0; i < iReturn; i++)
            {
                item = new ItemDesc(
                    dataSet.Tables[0].Rows[i]["ItemCode"].ToString(),
                    dataSet.Tables[0].Rows[i]["ItemDesc"].ToString(),
                    decimal.Parse(dataSet.Tables[0].Rows[i]["Cost"].ToString(), System.Globalization.NumberStyles.Currency)
                    );
                items.Add(item);
            }
        }

        /// <summary>
        /// Populates the list of invoice items from the current invoice based on the line items in the database.
        /// Uses selectable line items that have a selected property that the display window can use to delete items.
        /// </summary>
        public void PopulateInvoiceItems()
        {
            sSQL = clsMainSQL.SelectInvoiceItems(invoice.InvoiceNum.ToString());
            dataSet = db.ExecuteSQLStatement(sSQL, ref iReturn);

            SelectableLineItem lineItem;

            InvoiceItems.Clear();

            for (int i = 0; i < iReturn; i++)
            {
                lineItem = new SelectableLineItem(
                    dataSet.Tables[0].Rows[i]["ItemCode"].ToString(),
                    dataSet.Tables[0].Rows[i]["ItemDesc"].ToString(),
                    decimal.Parse(dataSet.Tables[0].Rows[i]["Cost"].ToString(), System.Globalization.NumberStyles.Currency),
                    false
                    );

                InvoiceItems.Add(lineItem);

                tempInvoiceItems.Clear();
                foreach (SelectableLineItem sLineItem in InvoiceItems)
                    tempInvoiceItems.Add(new SelectableLineItem(sLineItem));
                
                currentTotalCost = invoice.TotalCost;
                currentInvoiceDate = invoice.InvoiceDate;
            }
        }

        /// <summary>
        /// Adds a new item to the current invoice.
        /// </summary>
        /// <param name="item"></param>
        public void AddInvoiceItem(ItemDesc item)
        {
            if (item is null)
            {

            }else
            {
                InvoiceItems.Add(new SelectableLineItem(item.ItemCode, item.Desc, item.Cost, false));
                UpdateTotalCost();
            }
            
        }

        /// <summary>
        /// Deletes any currently selected invoice items.
        /// </summary>
        public void DeleteInvoiceItems()
        {
            for (int i = 0; i < InvoiceItems.Count; i++)
                if (InvoiceItems[i].Selected)
                {
                    InvoiceItems.Remove(InvoiceItems[i]);
                    i--;
                }
            UpdateTotalCost();
        }

        /// <summary>
        /// Updates the current total cost based on the price of every line item in the invoice.
        /// </summary>
        private void UpdateTotalCost()
        {
            currentTotalCost = 0;
            foreach (SelectableLineItem lineItem in InvoiceItems)
                currentTotalCost += lineItem.Cost;
        }

        /// <summary>
        /// Update the current invoice date and time.
        /// </summary>
        /// <param name="dateTime"></param>
        public void UpdateInvoiceDate(DateTime dateTime)
        {
            currentInvoiceDate = dateTime;
        }

        /// <summary>
        /// Clears any invoice items.
        /// </summary>
        public void ClearInvoiceItems()
        {
            InvoiceItems.Clear();
        }

        /// <summary>
        /// Removes all items in the items list. 
        /// Currently used for testing that the items list was update without performing a database change.
        /// </summary>
        public void DeleteItems()
        {
            items.Clear();
        }

        /// <summary>
        /// Sets the current invoice based on a given InvoiceID
        /// </summary>
        /// <param name="invoiceID">The number of the invoice</param>
        public void SetInvoice(int invoiceID)
        {
            SetInvoice(invoiceID.ToString());
        }

        /// <summary>
        /// Sets the invoice based on the proviced invoiceID
        /// </summary>
        /// <param name="invoiceID">ID of the invoice that is used to set the current invoice</param>
        private void SetInvoice(string invoiceID)
        {
            sSQL = clsMainSQL.SelectInvoice(invoiceID);
            dataSet = db.ExecuteSQLStatement(sSQL, ref iReturn);
            invoice = new Invoice(
                Int32.Parse(dataSet.Tables[0].Rows[0]["InvoiceNum"].ToString()),
                DateTime.Parse(dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString()),
                Int32.Parse(dataSet.Tables[0].Rows[0]["TotalCost"].ToString())
                );
        }

        /// <summary>
        /// Deletes the current invoice and all associated invoice items from the database.
        /// </summary>
        public void DeleteInvoice()
        {
            sSQL = clsMainSQL.DeleteFromLineItems(invoice.InvoiceNum.ToString());
            db.ExecuteNonQuery(sSQL);
            sSQL = clsMainSQL.DeleteFromInvoices(invoice.InvoiceNum.ToString());
            db.ExecuteNonQuery(sSQL);
            invoice = null;
            currentInvoiceDate = DateTime.Today;
            currentTotalCost = 0m;
            InvoiceItems.Clear();
            tempInvoiceItems.Clear();
        }

        /// <summary>
        /// If an invoice is saved, update the databases.
        /// Database behavior differs based on whether or not the invoice is new or not.
        /// </summary>
        /// <param name="isNewInvoice">Whether the invoice being saved is brand new or already exists.</param>
        public void SaveInvoice(bool isNewInvoice)
        {
            if (isNewInvoice)
            {
                // Create an invoice in the database.
                sSQL = clsMainSQL.InsertIntoInvoices(currentInvoiceDate.ToShortDateString(), currentTotalCost.ToString());
                db.ExecuteNonQuery(sSQL);

                // Get the invoice number of the newly created invoice and set the invoice object based on that new number.
                sSQL = clsMainSQL.SelectNewInvoiceNumber();
                SetInvoice(db.ExecuteScalarSQL(sSQL));

                // Add all line items for this invoice to the database.
                for (int i = 0; i < InvoiceItems.Count; i++)
                {
                    sSQL = clsMainSQL.InsertIntoLineItems(invoice.InvoiceNum.ToString(), (i + 1).ToString(), InvoiceItems[i].ItemCode);
                    db.ExecuteNonQuery(sSQL);
                }

                // Adjust the temporary list to match the current list
                tempInvoiceItems.Clear();
                foreach (SelectableLineItem sLineItem in InvoiceItems)
                    tempInvoiceItems.Add(new SelectableLineItem(sLineItem));

            }
            else
            {
                // Delete any existing line items for this invoice
                sSQL = clsMainSQL.DeleteFromLineItems(invoice.InvoiceNum.ToString());
                db.ExecuteNonQuery(sSQL);

                // Add all line items for this invoice to the database.
                for (int i = 0; i < InvoiceItems.Count; i++)
                {
                    sSQL = clsMainSQL.InsertIntoLineItems(invoice.InvoiceNum.ToString(), (i+1).ToString(), InvoiceItems[i].ItemCode);
                    db.ExecuteNonQuery(sSQL);
                }

                // Update the total cost of the invoice in the database.
                sSQL = clsMainSQL.SetTotalCost(invoice.InvoiceNum.ToString(), currentTotalCost.ToString());
                db.ExecuteNonQuery(sSQL);

                // Adjust the temporary list to match the current list
                tempInvoiceItems.Clear();
                foreach (SelectableLineItem sLineItem in InvoiceItems)
                    tempInvoiceItems.Add(new SelectableLineItem(sLineItem));
            }

            
        }

        /// <summary>
        /// Cancels any pending updates to the invoice.
        /// </summary>
        public void CancelSave()
        {
            if (invoice is null)
            {
                currentTotalCost = 0m;
                currentInvoiceDate = DateTime.Today;
            }
            else
                currentTotalCost = invoice.TotalCost;
            
            InvoiceItems.Clear();
            foreach (SelectableLineItem sLineItem in tempInvoiceItems)
                InvoiceItems.Add(new SelectableLineItem(sLineItem));
        }

        #region Getters

        /// <summary>
        /// The date of the current invoice.
        /// </summary>
        /// <returns>The date of the current invoice.</returns>
        public DateTime GetInvoiceDate()
        {
            return invoice.InvoiceDate;
        }

        /// <summary>
        /// Gets the total cost of the invoice.
        /// </summary>
        /// <returns></returns>
        public decimal GetInvoiceTotal()
        {
            return invoice.TotalCost;
        }

        /// <summary>
        /// Gets the item cost based on a specific item code from the current list of items.
        /// </summary>
        /// <param name="itemCode">The item code of the item whose cost we are returning.</param>
        /// <returns>The item cost of an item.</returns>
        public decimal GetItemCost(string itemCode)
        {
            for (int i = 0; i < Items.Count; i++)
                if (itemCode == Items[i].ItemCode)
                    return Items[i].Cost;

            return 0.0m;
        }

        /// <summary>
        /// Returns the invoice number. If the invoice is not set, return a default of -1.
        /// </summary>
        /// <returns>The current invoice number or -1 if no invoice.</returns>
        public int GetInvoiceNumber()
        {
            if (invoice is null)
                return -1;
            return invoice.InvoiceNum;
        }
        #endregion
        #endregion


        public class SelectableLineItem : INotifyPropertyChanged
        {
            private string sItemCode;
            private string sItemDescription;
            private decimal dItemCost;
            private bool bIsSelected;

            public SelectableLineItem(string itemCode, string desc, decimal cost, bool selected)
            {
                ItemCode = itemCode;
                Desc = desc;
                Cost = cost;
                Selected = selected;
            }

            public SelectableLineItem(SelectableLineItem selectableLineItem)
            {
                ItemCode = selectableLineItem.ItemCode;
                Desc = selectableLineItem.Desc;
                Cost = selectableLineItem.Cost;
                Selected = selectableLineItem.Selected;
            }

            public string ItemCode
            {
                get => sItemCode;
                set
                {
                    sItemCode = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ItemCode"));
                    }
                }
            }
            public string Desc
            {
                get => sItemDescription;
                set
                {
                    sItemDescription = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Desc"));
                    }
                }
            }

            public decimal Cost
            {
                get => dItemCost;
                set
                {
                    dItemCost = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Cost"));
                    }
                }
            }

            public bool Selected
            {
                get => bIsSelected;
                set
                {
                    bIsSelected = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            
        }
    }
}
