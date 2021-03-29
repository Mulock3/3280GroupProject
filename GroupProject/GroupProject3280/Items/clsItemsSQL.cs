using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject3280.Items
{
    public class clsItemsSQL
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
