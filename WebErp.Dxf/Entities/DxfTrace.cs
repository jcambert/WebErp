using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("TRACE")]
    public class DXFTrace : DxfEntity
    {
        private readonly DxfPoint extrusion ;
        private readonly DxfPoint[] corners ;

        public DXFTrace()
        {
            extrusion = new DxfPoint();
            corners = new DxfPoint[] { new DxfPoint(), new DxfPoint(), new DxfPoint() };
        }
        public DxfPoint ExtrusionDirection => extrusion; 
        
        public DxfPoint[] Corners=> corners; 

        public double Thickness { get; set; }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            if (groupcode >= 10 && groupcode <= 33)
            {
                int idx = groupcode % 10;
                int component = groupcode / 10;
                switch (component)
                {
                    case 1:
                        Corners[idx].X = double.Parse(value);
                        break;
                    case 2:
                        Corners[idx].Y = double.Parse(value);
                        break;
                    case 3:
                        Corners[idx].Z = double.Parse(value);
                        break;
                }
            }

            switch (groupcode)
            {
                case 39:
                    Thickness = double.Parse(value);
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
