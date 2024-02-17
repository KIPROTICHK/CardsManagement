using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagement.Application
{
    public static class SharedHelpers
    {
        public static bool StringCompare(this string b, string str)
        {
            if (string.IsNullOrEmpty(b) || string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (b.Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

    }
}
