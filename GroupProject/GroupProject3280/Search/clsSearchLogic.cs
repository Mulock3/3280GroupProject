using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// Database manager connection
        /// </summary>
        private DatabaseManager newManager = new DatabaseManager();
        /// <summary>
        /// Row Length for Dates
        /// </summary>
        public int TableRowSizeDates = 0;
        /// <summary>
        /// Row Length for IDs
        /// </summary>
        public int TableRowSizeIDs = 0;
        /// <summary>
        /// Row Length for Charges
        /// </summary>
        public int TableRowSizeCharges = 0;
        /// <summary>
        /// Row Length for the master data table
        /// </summary>
        public int TableRowSizeMasterTable = 0;
        /// <summary>
        /// Dataset for all invoice dates
        /// </summary>
        public List<string> allInvoiceDatesList = new List<string>();
        /// <summary>
        /// Dataset for all invoice ids
        /// </summary>
        public List<string> allInvoiceIDsList = new List<string>();
        /// <summary>
        /// Dataset for all charge amounts
        /// </summary>
        public List<string> allChargeAmountsList = new List<string>();
        /// <summary>
        /// Dataset for all invoices in master table
        /// </summary>
        public DataSet allInvoicesSelected;

        /// <summary>
        /// Basic set up for class, does nothing
        /// </summary>
        public clsSearchLogic()
        {
        }

        /// <summary>
        /// Updates all the data for the UI to use
        /// </summary>
        /// <param name="currentSelection"></param>
        internal void UpdateData(string currentSelection)
        {
            try
            {
                // Gathers the data we need for the UI
                DataSet allInvoiceDates = newManager.ExecuteSQLStatement(clsSearchSQL.GetInvoiceDates(), ref TableRowSizeDates);
                DataSet allInvoiceIDs = newManager.ExecuteSQLStatement(clsSearchSQL.GetInvoiceIDs(), ref TableRowSizeIDs);
                DataSet allChargeAmounts = newManager.ExecuteSQLStatement(clsSearchSQL.GetChargeAmounts(), ref TableRowSizeCharges);
                allInvoicesSelected = newManager.ExecuteSQLStatement(clsSearchSQL.getAllInvoicesSQL, ref TableRowSizeMasterTable);

                // Makes the lists for databinding purposes
                allInvoiceDatesList.Clear();
                foreach(DataRow x in allInvoiceDates.Tables[0].Rows)
                {
                    allInvoiceDatesList.Add(x[0].ToString());
                }
                allInvoiceDatesList.Add("");

                allInvoiceIDsList.Clear();
                foreach (DataRow x in allInvoiceIDs.Tables[0].Rows)
                {
                    allInvoiceIDsList.Add(x[0].ToString());
                }
                allInvoiceIDsList.Add("");

                allChargeAmountsList.Clear();
                foreach (DataRow x in allChargeAmounts.Tables[0].Rows)
                {
                    allChargeAmountsList.Add(x[0].ToString());
                }
                allChargeAmountsList.Add("");

                // This will return the current selection the user last had and update it as well
                if (currentSelection.Trim() != "")
                {
                    allInvoicesSelected = newManager.ExecuteSQLStatement(currentSelection, ref TableRowSizeMasterTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Takes the info and updates the currentSelection based upon the data taken in 
        /// </summary>
        /// <param name="dateIN"></param>
        /// <param name="idIN"></param>
        /// <param name="chargeIN"></param>
        internal string DateIDChargeSelection(string dateIN, string idIN, string chargeIN)
        {
            try
            {
                string currentSelection = "";
                // Sets it back to normal
                if (dateIN == "" && idIN == "" && chargeIN == "")
                {
                    currentSelection = clsSearchSQL.getAllInvoicesSQL;
                }
                // Date Only
                else if (idIN == "" && chargeIN == "")
                {
                    // Gets the data from current date
                    DateTime newTime = DateTime.Parse(dateIN);
                    SqlDateTime newDate = new SqlDateTime(newTime);
                    currentSelection = clsSearchSQL.GetInvoiceDataFromDate(newDate.Value.ToString());

                }
                // Charge Only
                else if (dateIN == "" && idIN == "")
                {
                    // Gets the data from current Charge
                    currentSelection = clsSearchSQL.GetInvoiceDataFromCost(chargeIN);

                }
                // ID Only
                else if (dateIN == "" && chargeIN == "")
                {
                    // Gets the data from current ID
                    currentSelection = clsSearchSQL.GetInvoiceDataFromID(idIN);

                }
                // ID And Date
                else if (chargeIN == "")
                {
                    // Gets the data from current ID and Date
                    DateTime newTime = DateTime.Parse(dateIN);
                    SqlDateTime newDate = new SqlDateTime(newTime);
                    currentSelection = clsSearchSQL.GetInvoiceDataFromIDandDate(idIN, newDate.Value.ToString());
                }
                // Date And Charge
                else if (idIN == "")
                {
                    // Gets the data from current Date and Charge
                    DateTime newTime = DateTime.Parse(dateIN);
                    SqlDateTime newDate = new SqlDateTime(newTime);
                    currentSelection = clsSearchSQL.GetInvoiceDataFromCostandDate(chargeIN, newDate.Value.ToString());

                }
                // Charge And ID
                else if (dateIN == "")
                {
                    // Gets the data from current ID and Charge
                    currentSelection = clsSearchSQL.GetInvoiceDataFromIDandCost(idIN, chargeIN);

                }
                // Date, ID, Charge
                else
                {
                    DateTime newTime = DateTime.Parse(dateIN);
                    SqlDateTime newDate = new SqlDateTime(newTime);
                    currentSelection = clsSearchSQL.GetInvoiceDataFromCostandDateandID(idIN, chargeIN, newDate.Value.ToString());
                }

                return currentSelection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
