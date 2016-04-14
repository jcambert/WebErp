using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfDimStyleRecord : DxfRecord
    {
        internal DxfDimStyleRecord():base()
        {

        }
        public string StyleName { get; set; }
    }
}
