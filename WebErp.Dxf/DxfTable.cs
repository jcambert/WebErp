using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;
using WebErp.Dxf.Parsers;

namespace WebErp.Dxf
{
    public class DxfTables
    {

        private readonly List<DxfAppIDRecord> appids ;
        private readonly List<DxfBlockRecord> blocks;
        private readonly List<DxfDimStyleRecord> dimstyles ;
        private readonly List<DxfLayerRecord> layers ;
        private readonly List<DxfLineTypeRecord> linetypes ;
        private readonly List<DxfUCSRecord> ucs ;
        private readonly List<DxfViewRecord> views ;
        private readonly List<DxfVPortRecord> vports;

        internal DxfTables()
        {
            appids = new List<DxfAppIDRecord>();
            blocks = new List<DxfBlockRecord>();
            dimstyles = new List<DxfDimStyleRecord>();
            layers = new List<DxfLayerRecord>();
            linetypes = new List<DxfLineTypeRecord>();
            ucs = new List<DxfUCSRecord>();
            views = new List<DxfViewRecord>();
            vports = new List<DxfVPortRecord>();

        }
        
        [Table("APPID", typeof(DxfAppIDParser))]
        public List<DxfAppIDRecord> AppIDs { get { return appids; } }

       
        [Table("BLOCK_RECORD", typeof(DxfBlockRecordParser))]
        public List<DxfBlockRecord> Blocks { get { return blocks; } }

        
        [Table("DIMSTYLE", typeof(DxfDimStyleRecordParser))]
        public List<DxfDimStyleRecord> DimStyles { get { return dimstyles; } }

        
        [Table("LAYER", typeof(DxfLayerRecordParser))]
        public List<DxfLayerRecord> Layers { get { return layers; } }

        
        [Table("LTYPE", typeof(DxfLineTypeRecordParser))]
        public List<DxfLineTypeRecord> LineTypes { get { return linetypes; } }

        private readonly List<DxfStyleRecord> styles = new List<DxfStyleRecord>();
        [Table("STYLE", typeof(DxfStyleRecordParser))]
        public List<DxfStyleRecord> Styles { get { return styles; } }

        
        [Table("UCS", typeof(DxfUCSRecordParser))]
        public List<DxfUCSRecord> UCS { get { return ucs; } }

        
        [Table("VIEW", typeof(DxfViewRecordParser))]
        public List<DxfViewRecord> Views { get { return views; } }

        
        [Table("VPORT", typeof(DxfVPortRecordParser))]
        public List<DxfVPortRecord> VPorts { get { return vports; } }
    }
}
