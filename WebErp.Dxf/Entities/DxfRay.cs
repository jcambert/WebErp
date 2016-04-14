using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("RAY")]
    public class DXFRay : DxfEntity
    {
        private DxfPoint startpoint = new DxfPoint();
        public DxfPoint Start { get { return startpoint; } }

        private DxfPoint direction = new DxfPoint();
        public DxfPoint Direction { get { return direction; } }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 10:
                    Start.X = double.Parse(value);
                    break;
                case 20:
                    Start.Y = double.Parse(value);
                    break;
                case 30:
                    Start.Z = double.Parse(value);
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
            }
        }
    }

    [Entity("XLINE")]
    public class DxfXLine : DXFRay
    {
    }
}
