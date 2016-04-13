using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string s)
        {
            if (s.IsNull()) return true;
            return s.Trim().Length == 0;
        }
    }
}
