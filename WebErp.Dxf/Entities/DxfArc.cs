﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("ARC")]
    public class DxfArc : DxfEntity
    {
        public double Thickness { get; set; }
        private DxfPoint center = new DxfPoint();
        public DxfPoint Center => center;

        public double Radius { get; set; }

        public double StartAngle { get; set; }
        public double EndAngle { get; set; }
        
        private DxfPoint extrusion = new DxfPoint();
        public DxfPoint ExtrusionDirection { get { return extrusion; } }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 39:
                    Thickness = double.Parse(value);
                    break;
                case 10:
                    Center.X = double.Parse(value);
                    break;
                case 20:
                    Center.Y = double.Parse(value);
                    break;
                case 30:
                    Center.Z = double.Parse(value);
                    break;
                case 40:
                    Radius = double.Parse(value);
                    break;
                case 50:
                    StartAngle = double.Parse(value);
                    break;
                case 51:
                    EndAngle = double.Parse(value);
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
