using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280
{
    #region DATABASE_OBJECTS

    /// <summary>
    /// Represents an entry in the Invoices table of the database
    /// </summary>
    public class Invoice {
        /// <summary>The Invoice number</summary>
        public readonly int InvoiceNum;
        /// <summary>The Invoice creation date</summary>
        public readonly DateTime InvoiceDate;
        /// <summary>The Invoice total cost</summary>
        public readonly int TotalCost;

        /// <summary>Invoice constructor</summary>
        /// <param name="pInvoiceNum">The Invoice number</param>
        /// <param name="pInvoiceDate">The Invoice creation date</param>
        /// <param name="pTotalCost">The Invoice total cost</param>
        public Invoice(int pInvoiceNum, DateTime pInvoiceDate, int pTotalCost) {
            InvoiceNum = pInvoiceNum;
            InvoiceDate = pInvoiceDate;
            TotalCost = pTotalCost;
        }
    }

    /// <summary>
    /// Represents an entry in the ItemDesc table of the database
    /// </summary>
    public class ItemDesc {
        /// <summary>The Item Number associated with the description</summary>
        public readonly string ItemCode;
        /// <summary>The actual description text</summary>
        public readonly string Desc;
        /// <summary>The monetary cost of a single item</summary>
        public readonly decimal Cost;

        /// <summary>ItemDesc constructor</summary>
        /// <param name="pItemCode">The Item Number of the item with this description</param>
        /// <param name="pDesc">The actual description text</param>
        /// <param name="pCost">The monetary cost of a single item</param>
        public ItemDesc(string pItemCode, string pDesc, decimal pCost) {
            ItemCode = pItemCode;
            Desc = pDesc;
            Cost = pCost;
        }

        /// <summary>String representation of this object</summary>
        /// <returns>The item code and description</returns>
        public override string ToString() {
            return ItemCode + " " + Desc;
        }
    }

    /// <summary>
    /// Represents an entry in the LineItems table of the database
    /// </summary>
    public class LineItem {
        /// <summary>The Invoice Num of the item</summary>
        public readonly int InvoiceNum;
        /// <summary>The Line Item Num (used for position in datagrid)</summary>
        public readonly int LineItemNum;
        /// <summary>The Item Code of the item</summary>
        public readonly string ItemCode;

        /// <summary>LineItem constructor</summary>
        /// <param name="pInvoiceNum">The Invoice Num of the item</param>
        /// <param name="pLineItemNum">The Line Item Num</param>
        /// <param name="pItemCode">The Item Code of the item</param>
        public LineItem(int pInvoiceNum, int pLineItemNum, string pItemCode) {
            InvoiceNum = pInvoiceNum;
            LineItemNum = pLineItemNum;
            ItemCode = pItemCode;
        }
    }

    #endregion

    #region DATABASE_MANAGER

    /// <summary>
    /// Class used to access the database.
    /// </summary>
    public class DatabaseManager
    {
        #region ATTRIBUTES    

        /// <summary>Connection string to the database</summary>
        private string sConnectionString;

        #endregion

        #region METHODS

        /// <summary>DBManager constructor</summary>
        public DatabaseManager() {
            // Note: the Microsoft.ACE.OLEDB.12.0 Provider is required for .accdb files
            sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.accdb";
        }

        #region EXECUTE_SQL_METHODS

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
        public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal) {
            try {
                //Create a new DataSet
                DataSet ds = new DataSet();

                // "using" is guaranteed to "dispose" (close) the object when leaving scope
                using (OleDbConnection conn = new OleDbConnection(sConnectionString)) {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter()) {
                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            } catch (Exception ex) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
        public string ExecuteScalarSQL(string sSQL) {
            try {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString)) {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter()) {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null) {
                    //Return a blank
                    return "";
                } else {
                    //Return the value
                    return obj.ToString();
                }
            } catch (Exception ex) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
		public int ExecuteNonQuery(string sSQL) {
            try {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString)) {
                    // No OleDbDataAdapter because it's not needed for non-query

                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            } catch (Exception ex) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
