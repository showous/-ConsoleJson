using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schneider.Frame;

namespace Schneider.Andon
{
    public class AndonUtility
    {
        public static string GetItemsSeting(StructBase[] items) {
            string r = "";
            for (int i = 0; i < items.Length; i++)
            {
                if (string.IsNullOrEmpty(r)) r += ",";
                r += "items[" + i + "]:" + items[i].ToString();
            }

            return r;
        }

        public static string GetStrings(string[] items)
        {
            string r = "";
            for (int i = 0; i < items.Length; i++)
            {
                if (string.IsNullOrEmpty(r)) r += ",";
                r += items[i];
            }

            return "[" + r + "]";
        }
    }
}
