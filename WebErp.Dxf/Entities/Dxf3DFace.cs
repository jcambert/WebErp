using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("3DFACE")]
    public class DXF3DFace : DxfEntity
    {
        private DxfPoint[] corners = new DxfPoint[] { new DxfPoint(), new DxfPoint(), new DxfPoint() };
        public DxfPoint[] Corners => corners;

        [Flags]
        public enum FlagsEnum
        {
            FirstEdgeInvisible = 1,
            SecondEdgeInvisible = 2,
            ThirdEdgeInvisible = 4,
            FourthEdgeInvisible = 8
        }

        public FlagsEnum Flags { get; set; }

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
            else if (groupcode == 70)
            {
                Flags = (FlagsEnum)Enum.Parse(typeof(FlagsEnum), value);
            }
        }
    }
}
