using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfAppIDRecord:DxfRecord
    {
        internal DxfAppIDRecord():base()
        {

        }
        public string ApplicationName { get; set; }
    }


}
