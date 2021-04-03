using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Main
{
    public static class clsMainSQL
    {

        /// <summary>
        /// SQL query to update the total cost of the invoice
        /// </summary>
        /// <param name="invoiceNumber">Number of the invoice that is being updated</param>
        /// <param name="totalCost">New cost of the invoice</param>
        /// <returns>SQL query string to update the total cost of the invoce based on the invoice number</returns>
        public static string SetTotalCost(string invoiceNumber, string totalCost)
        {
            return "UPDATE Invoices SET TotalCost = " + totalCost + " WHERE InvoiceNum = " + invoiceNumber;
        }

        /// <summary>
        /// SQL query to delete any line items from a specific invoice
        /// </summary>
        /// <param name="invoiceNumber">Number of the invoice that is being updated</param>
        /// <returns>SQL query string to delete any line items from a specific invoice</returns>
        public static string DeleteFromLineItems(string invoiceNumber)
        {
            return "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNumber;
        }

        /// <summary>
        /// SQL query to delete an invoice based on its number
        /// </summary>
        /// <param name="invoiceNumber">Number of the invoice that is being updated</param>
        /// <returns>SQL query string to delete an invoice based on its number</returns>
        public static string DeleteFromInvoices(string invoiceNumber)
        {
            return "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNumber;
        }

        /// <summary>
        /// SQL query to insert a row into the Line Items table using the invoice date, the line item number and the item code
        /// </summary>
        /// <param name="invoiceNumber">Number of the invoice that the item belongs to</param>
        /// <param name="lineItemNumber">The line number of the data list</param>
        /// <param name="itemCode">The code of the item</param>
        /// <returns>SQL query string to insert a row into the Line Items table using the invoice date, the line item number and the item code</returns>
        public static string InsertIntoLineItems(string invoiceNumber, string lineItemNumber, string itemCode)
        {
            return "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values ("+ invoiceNumber + ", " + lineItemNumber + ", '" + itemCode + "')";
        }

        /// <summary>
        /// SQL query to insert a row into the Invoice table using the invoice date and the total cost
        /// </summary>
        /// <param name="invoiceDate">Date of the invoice</param>
        /// <param name="totalCost">Total cost of all items contained in the invoice</param>
        /// <returns>SQL query string to insert a row into the Invoice table using the invoice date and the total cost</returns>
        public static string InsertIntoInvoices(string invoiceDate, string totalCost)
        {
            return "INSERT INTO Invoices (InvoiceDate, TotalCost) Values('#" + invoiceDate + "#', " + totalCost + ")";
        }

        /// <summary>
        /// SQL query to select an invoice based on the invoice number
        /// </summary>
        /// <param name="invoiceNumber">Number of the invoice being selected</param>
        /// <returns>SQL query string to select an invoice based on the invoice number</returns>
        public static string SelectInvoice(string invoiceNumber)
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNumber;
        }

        /// <summary>
        /// SQL query to select all items
        /// </summary>
        /// <returns>SQL query string to select all items</returns>
        public static string SelectItems()
        {
            return "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
        }

        /// <summary>
        /// SQL qeury to select the items in an invoice
        /// </summary>
        /// <param name="invoiceNumber">The number of the invoice that contains the items being returned</param>
        /// <returns>SQL qeury to select the items in an invoice</returns>
        public static string SelectInvoiceItems(string invoiceNumber)
        {
            return "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + invoiceNumber;
        }
    }
}
