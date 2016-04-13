﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("SHAPE")]
    internal class DXFShape : DxfEntity
    {
        public double Thickness { get; set; }
        private DxfPoint insertion = new DxfPoint();
        public DxfPoint InsertionPoint { get { return insertion; } }
        public double Size { get; set; }
        public string ShapeName { get; set; }
        public double RotationAngle { get; set; }
        public double RelativeXScale { get; set; }
        public double ObliqueAngle { get; set; }
        private DxfPoint extrusion = new DxfPoint();
        public DxfPoint ExtrusionDirection { get { return extrusion; } }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 39:
                    Thickness = double.Parse(value);
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
                case 40:
                    Size = double.Parse(value);
                    break;
                case 2:
                    ShapeName = value;
                    break;
                case 50:
                    RotationAngle = double.Parse(value);
                    break;
                case 41:
                    RelativeXScale = double.Parse(value);
                    break;
                case 51:
                    ObliqueAngle = double.Parse(value);
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
