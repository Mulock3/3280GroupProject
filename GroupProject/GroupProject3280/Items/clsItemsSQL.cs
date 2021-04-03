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

        

    }
}
