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
        private const string sqlGetItemDesc = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";

        /// <summary>
        /// Returns the SQL statement to get all item descriptions
        /// </summary>
        /// <returns>The SQL to get all item descriptions</returns>
        public static string GetItemDesc() {
            return sqlGetItemDesc;
        }

        public static List<ItemDesc> GetItemDesc(DatabaseManager database) {
            List<ItemDesc> list = new List<ItemDesc>();
            try {
                DataSet ds;
                int rowCount = 0;
                // execute SQL statement
                ds = database.ExecuteSQLStatement(GetItemDesc(), ref rowCount);
                // populate the list
                for (int i = 0; i < rowCount; i++) {
                    list.Add(new ItemDesc((string)ds.Tables[0].Rows[i][0], (string)ds.Tables[0].Rows[i][1], (decimal)ds.Tables[0].Rows[i][2]));
                }
            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
            return list;
        }

    }
}
