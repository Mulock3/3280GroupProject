using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Main
{
    class clsMainLogic
    {
        /// <summary>
        /// Database manager class that allows us to connect and perform queries and return information from a database.
        /// </summary>
        private DatabaseManager db;

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
        /// List of items that will be used when adding or removing from an invoice.
        /// </summary>
        private readonly ObservableCollection<ItemDesc> items;

        /// <summary>
        /// Publicly accessible list of items that will be used when adding or removing from an invoice.
        /// </summary>
        public ObservableCollection<ItemDesc> Items { get => items; }

        /// <summary>
        /// Constructor that initializes the database manager and populates the current list of items.
        /// </summary>
        public clsMainLogic()
        {
            db = new DatabaseManager();
            dataSet = new DataSet();
            items = new ObservableCollection<ItemDesc>();
            PopulateItemsList();
        }

        /// <summary>
        /// Runs a sql command to retreive all current items in the database and adds them to the list of items.
        /// </summary>
        private void PopulateItemsList()
        {
            sSQL = clsMainSQL.SelectItems();
            dataSet = db.ExecuteSQLStatement(sSQL, ref iReturn);

            ItemDesc item;

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
        /// Removes all items in the items list. 
        /// Currently used for testing that the items list was update without performing a database change.
        /// </summary>
        public void DeleteItems()
        {
            items.Clear();
        }
    }
}
