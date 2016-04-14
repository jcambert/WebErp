using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("TOLERANCE")]
    public class DXFTolerance : DxfEntity
    {
        private readonly DxfPoint insertion ;
        private readonly DxfPoint extrusion ;
        private readonly DxfPoint direction ;
        internal DXFTolerance():base()
        {
            insertion = new DxfPoint();
            extrusion = new DxfPoint();
            direction = new DxfPoint();
        }
        public string DimensionStyle { get; set; }
        
        public DxfPoint InsertionPoint=> insertion; 
        
        public DxfPoint ExtrusionDirection => extrusion; 
        
        public DxfPoint Direction =>direction; 

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 3:
                    DimensionStyle = value;
                    break;
                case 10:
                    InsertionPoint.X = double.Parse(value);
                    break;
                case 20:
                    InsertionPoint.Y = double.Parse(value);
                    break;
                case 30:
                    InsertionPoint.Z = double.Parse(value);
                    break;
                case 11:
                    Direction.X = double.Parse(value);
                    break;
                case 21:
                    Direction.Y = double.Parse(value);
                    break;
                case 31:
                    Direction.Z = double.Parse(value);
                    break;
                case 210:
                    ExtrusionDirection.X = double.Parse(value);
                    break;
                case 220:
                    ExtrusionDirection.Y = double.Parse(value);
                    break;
                case 230:
                    ExtrusionDirection.Z = double.Parse(value);
                    break;
            }
        }
    }
}
