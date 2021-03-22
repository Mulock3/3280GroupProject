using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Search
{
    public static class clsSearchSQL
    {
        private static string getAllInvoicesSQL = "SELECT* FROM Invoices";

        #region SQL Preperations Statements
        /// <summary>
        /// Returns the SQL for getting invoice data from specific invoice number
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromID(string idIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + idIN;

            return sSQL;
        }

        /// <summary>
        /// Returns the SQL for getting invoice data from specific cost amount
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromCost(string costIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + costIN;

            return sSQL;
        }

        /// <summary>
        /// Returns the SQL for getting invoice data from specific data
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromDate(string dateIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = " + dateIN;

            return sSQL;
        }

        /// <summary>
        /// Returns the SQL for getting invoice data from invoice number and cost
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromIDandCost(string idIN, string costIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + idIN + " AND TotalCost = " + costIN;

            return sSQL;
        }

        /// <summary>
        /// Returns the SQL for getting invoice data from invoice number and date
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromIDandDate(string idIN, string dateIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + idIN + " AND InvoiceDate = " + dateIN;

            return sSQL;
        }

        /// <summary>
        /// Returns the SQL for getting invoice data from invoice cost and date
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromCostandDate(string costIN, string dateIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + costIN + " AND InvoiceDate = " + dateIN;

            return sSQL;
        }

        /// <summary>
        /// Returns the SQL for getting invoice data from invoice cost and date
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public static string GetInvoiceDataFromCostandDateandID(string idIN, string costIN, string dateIN)
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + costIN + " AND InvoiceDate = " + dateIN + " AND InvoiceNum = " + idIN;

            return sSQL;
        }
        #endregion
    }
}
