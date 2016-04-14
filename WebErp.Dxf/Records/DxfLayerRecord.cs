using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfLayerRecord : DxfRecord
    {
        internal DxfLayerRecord():base()
        {

        }
        public string LayerName { get; set; }
        public int Color { get; set; }
        public string LineType { get; set; }
    }
}

