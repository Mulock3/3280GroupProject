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
        private DatabaseManager database;

        public clsItemsLogic(DatabaseManager pDatabase) {
            database = pDatabase;
        }

        /// <summary>
        /// Attempts to delete an item desc from the database
        /// </summary>
        /// <param name="pItemCode">The item code to delete</param>
        /// <returns>True if the item desc was deleted</returns>
        public bool DeleteItem(string pItemCode) {

            // if the item is present on any invoice, do not delete
            if(GetInvoicesFor(pItemCode).Count > 0) {
                return false;
            }
            // TODO delete from database

            return true;
        }


        public bool AddItemDesc(string pItemCode, string pItemDesc, decimal pCost) {
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = database.ExecuteSQLStatement(clsItemsSQL.AddItemDesc(pItemCode, pItemDesc, pCost), ref rowCount);
                // TODO push changes to database?

            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return true;
        }

        public bool UpdateItemDesc(string pItemCode, string pItemDesc, decimal pCost) {
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = database.ExecuteSQLStatement(clsItemsSQL.UpdateItemDesc(pItemCode, pItemDesc, pCost), ref rowCount);
                // TODO push changes to database?

            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return true;
        }

        /// <summary>
        /// Queries the database for all ItemDesc objects
        /// </summary>
        /// <returns>A List of ItemDesc objects</returns>
        public List<ItemDesc> GetItemDesc() {
            List<ItemDesc> list = new List<ItemDesc>();
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = database.ExecuteSQLStatement(clsItemsSQL.GetItemDesc(), ref rowCount);
                // populate the list
                for (int i = 0; i < rowCount; i++) {
                    list.Add(new ItemDesc((string)ds.Tables[0].Rows[i][0], (string)ds.Tables[0].Rows[i][1], (decimal)ds.Tables[0].Rows[i][2]));
                }
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return list;
        }

        public List<Invoice> GetInvoicesFor(string pItemCode) {
            List<Invoice> list = new List<Invoice>();
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = database.ExecuteSQLStatement(clsItemsSQL.GetItemDesc(), ref rowCount);
                // populate the list
                for (int i = 0; i < rowCount; i++) {
                    list.Add(new Invoice((Int32)ds.Tables[0].Rows[i][0], (DateTime)ds.Tables[0].Rows[i][1], (Int32)ds.Tables[0].Rows[i][2]));
                }
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return list;
        }

    }
}
