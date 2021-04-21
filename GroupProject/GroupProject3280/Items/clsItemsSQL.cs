using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Items
{
    public static class clsItemsSQL
    {
        /// <summary>SQL format to list item descriptions</summary>
        private const string sqlGetItemDesc = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        /// <summary>SQL format to list invoices containing an item</summary>
        private const string sqlGetInvoicesFromItem = "SELECT DISTINCT(InvoiceNum) FROM LineItems WHERE ItemCode = '{0}'";
        /// <summary>SQL format to update an item description</summary>
        private const string sqlUpdateItemDesc = "UPDATE ItemDesc SET ItemDesc = '{0}', Cost = {1} WHERE ItemCode = '{2}'";
        /// <summary>SQL format to add an item description</summary>
        private const string sqlAddItem = "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUES('{0}', '{1}', {2})";
        /// <summary>SQL format to delete an item description</summary>
        private const string sqlDeleteItem = "DELETE FROM ItemDesc WHERE ItemCode = '{0}'";
        /// <summary>SQL format to list item descriptions containing an item</summary>
        private const string sqlGetItemDescFromItem = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc WHERE ItemCode = '{0}'";


        /// <summary>
        /// Creates an SQL statement to get all item descriptions
        /// </summary>
        /// <returns>The SQL statement</returns>
        public static string GetItemDesc() {
            return sqlGetItemDesc;
        }

        /// <summary>
        /// Creates an SQL statement to get all invoices that
        /// contain the given item
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <returns>The SQL statement</returns>
        public static string GetInvoicesFromItem(string pItemCode) {
            return String.Format(sqlGetInvoicesFromItem, pItemCode);
        }

        /// <summary>
        /// Creates an SQL statement to update the item desc
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <param name="pItemDesc">The new description of the item</param>
        /// <param name="pCost">The new cost of the item</param>
        /// <returns>The SQL statement</returns>
        public static string UpdateItemDesc(string pItemCode, string pItemDesc, decimal pCost) {
            return String.Format(sqlUpdateItemDesc, pItemDesc, pCost, pItemCode);
        }

        /// <summary>
        /// Creates an SQL statement to add a new item desc
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <param name="pItemDesc">The description of the item</param>
        /// <param name="pCost">The cost of the item</param>
        /// <returns>The SQL statement</returns>
        public static string AddItemDesc(string pItemCode, string pItemDesc, decimal pCost) {
            return String.Format(sqlAddItem, pItemCode, pItemDesc, pCost);
        }

        /// <summary>
        /// Creates an SQL statement to delete an item desc
        /// </summary>
        /// <param name="pItemCode">The item code</param>
        /// <returns>The SQL statement</returns>
        public static string DeleteItemDesc(string pItemCode) {
            return String.Format(sqlDeleteItem, pItemCode);
        }

        public static string GetItemDescFromItem(string pItemCode) {
            return String.Format(sqlGetItemDescFromItem, pItemCode);
        }

    }
}
