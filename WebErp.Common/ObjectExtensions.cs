using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp
{
    public static class ObjectExtensions
    {
        [Pure]
        public static bool IsNull(this object o)
        {
            return o == null;
        }

        [Pure]
        public static bool IsNotNull(this object o)
        {
            return !o.IsNull() ;
        }
    }
}
