using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfBlockRecord : DxfRecord
    {
        internal DxfBlockRecord():base()
        {

        }
        public string BlockName { get; set; }
    }
}
