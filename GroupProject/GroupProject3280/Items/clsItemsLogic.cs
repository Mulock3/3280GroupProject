using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Items
{
    public class clsItemsLogic
    {
        #region ATTRIBUTES

        /// <summary>A reference to the database connection</summary>
        private DatabaseManager Database;

        #endregion

        #region METHODS

        /// <summary>clsItemsLogic constructor</summary>
        /// <param name="pDatabase">The database connection to use</param>
        public clsItemsLogic(DatabaseManager pDatabase) {
            Database = pDatabase;
        }


        /// <summary>
        /// Checks if the given string is a valid item code.
        /// Specifically, ensures the item code doesn't already exist.
        /// </summary>
        /// <param name="pItemCode">The item code string</param>
        /// <returns>True if the string is valid</returns>
        public bool ValidateAddCode(string pItemCode) {
            return pItemCode != null && pItemCode.Length > 0 && GetItemDesc(pItemCode).Count == 0;
        }

        /// <summary>
        /// Checks if the given string is a valid description.
        /// Specifically, ensures the description is not empty
        /// </summary>
        /// <param name="pItemDesc">The description string</param>
        /// <returns>True if the string is valid</returns>
        public bool ValidateDescString(string pItemDesc) {
            return pItemDesc != null && pItemDesc.Length > 0;
        }

        /// <summary>
        /// Checks if the given string is a valid cost.
        /// Specifically, ensures the string is a positive decimal number
        /// </summary>
        /// <param name="pItemCost">The cost string</param>
        /// <returns>The cost value, or -1 if the string is invalid</returns>
        public bool ValidateCostString(string pItemCost, out Decimal pItemCostDec) {
            decimal dec;
            if(Decimal.TryParse(pItemCost, out dec) && dec >= 0) {
                pItemCostDec = dec;
                return true;
            } else {
                pItemCostDec = -1;
                return false;
            }
        }

        #region DATABASE_METHODS


        /// <summary>
        /// Attempts to delete an item desc from the database
        /// </summary>
        /// <param name="pItemCode">The item code to delete</param>
        /// <returns>True if the item desc was deleted</returns>
        public bool DeleteItem(string pItemCode) {
            try {
                // if the item is present on any invoice, do not delete
                if (GetInvoicesFor(pItemCode).Count > 0) {
                    return false;
                }
                int rowCount = 0;
                // execute SQL statement
                rowCount = Database.ExecuteNonQuery(clsItemsSQL.DeleteItemDesc(pItemCode));
                return rowCount > 0;
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Adds an ItemDesc object to the database
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <param name="pItemDesc">The item description</param>
        /// <param name="pCost">The item cost</param>
        /// <returns>True if the database was modified</returns>
        public bool AddItemDesc(string pItemCode, string pItemDesc, decimal pCost) {
            try {
                int rowCount = 0;
                // execute SQL statement
                rowCount = Database.ExecuteNonQuery(clsItemsSQL.AddItemDesc(pItemCode, pItemDesc, pCost));
                return rowCount > 0;
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Updates the database with an item desc
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <param name="pItemDesc">The updated item description</param>
        /// <param name="pCost">The updated item cost</param>
        /// <returns>True if the database was modified</returns>
        public bool UpdateItemDesc(string pItemCode, string pItemDesc, decimal pCost) {
            try {
                int rowCount = 0;
                // execute SQL statement
                rowCount = Database.ExecuteNonQuery(clsItemsSQL.UpdateItemDesc(pItemCode, pItemDesc, pCost));
                return rowCount > 0;
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Queries the database for all ItemDesc objects
        /// </summary>
        /// <returns>A List of ItemDesc objects. May be empty, but shouldn't be!</returns>
        public List<ItemDesc> GetItemDesc() {
            List<ItemDesc> list = new List<ItemDesc>();
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = Database.ExecuteSQLStatement(clsItemsSQL.GetItemDesc(), ref rowCount);
                // populate the list
                for (int i = 0; i < rowCount; i++) {
                    list.Add(new ItemDesc((string)ds.Tables[0].Rows[i][0], (string)ds.Tables[0].Rows[i][1], (decimal)ds.Tables[0].Rows[i][2]));
                }
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return list;
        }

        /// <summary>
        /// Queries the database for the ItemDesc
        /// containing the given item code
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <returns>A list that is either empty or contains the requested ItemDesc</returns>
        public List<ItemDesc> GetItemDesc(string pItemCode) {
            List<ItemDesc> list = new List<ItemDesc>();
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = Database.ExecuteSQLStatement(clsItemsSQL.GetItemDescFromItem(pItemCode), ref rowCount);
                // populate the list
                for (int i = 0; i < rowCount; i++) {
                    list.Add(new ItemDesc((string)ds.Tables[0].Rows[i][0], (string)ds.Tables[0].Rows[i][1], (decimal)ds.Tables[0].Rows[i][2]));
                }
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return list;
        }

        /// <summary>
        /// Queries the database for all InvoiceNums of Invoices
        /// containing the given Item Code
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <returns>A List of Invoice objects. May be empty</returns>
        public List<int> GetInvoicesFor(string pItemCode) {
            List<int> list = new List<int>();
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = Database.ExecuteSQLStatement(clsItemsSQL.GetInvoicesFromItem(pItemCode), ref rowCount);
                // populate the list
                for (int i = 0; i < rowCount; i++) {
                    list.Add((int)ds.Tables[0].Rows[i][0]);
                }
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return list;
        }

        #endregion

        #endregion
    }
}
