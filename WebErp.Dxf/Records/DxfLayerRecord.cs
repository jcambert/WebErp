using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    internal class DxfLayerRecord : DxfRecord
    {
        public string LayerName { get; set; }
        public int Color { get; set; }
        public string LineType { get; set; }
    }
}

