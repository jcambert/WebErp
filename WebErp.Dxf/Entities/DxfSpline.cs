using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("SPLINE")]
    public class DXFSpline : DxfEntity
    {
        private readonly DxfPoint normal ;
        private readonly DxfPoint starttangent;
        private readonly DxfPoint endtangent ;
        private readonly List<double> knotvalues ;
        private readonly List<DxfPoint> controlpoints ;
        private readonly List<DxfPoint> fitpoints ;
        public DXFSpline():base()
        {
            normal = new DxfPoint();
            starttangent = new DxfPoint();
            endtangent = new DxfPoint();
            knotvalues = new List<double>();
            controlpoints = new List<DxfPoint>();
            fitpoints = new List<DxfPoint>();
        }
        public DxfPoint Normal => normal;

        [Flags]
        public enum FlagsEnum
        {
            Closed = 1,
            Periodic = 2,
            Rational = 4,
            Planar = 8,
            Linear = 16
        }
        public FlagsEnum Flags { get; set; }

        public int Degree { get; set; }
        public int KnotCount { get; set; }
        public int ControlPointCount { get; set; }
        public int FitPointCount { get; set; }

        public double KnotTolerance { get; set; }
        public double ControlPointTolerance { get; set; }
        public double FitPointTolerance { get; set; }

        
        public DxfPoint StartTangent => starttangent; 

        
        public DxfPoint EndTangent => endtangent; 

        
        public List<double> KnotValues => knotvalues; 

        public double Weight { get; set; }

        
        public List<DxfPoint> ControlPoints => controlpoints; 

        
        public List<DxfPoint> FitPoints =>fitpoints; 

        private DxfPoint LastControlPoint
        {
            get
            {
                if (ControlPoints.Count == 0) return null;
                return ControlPoints[ControlPoints.Count - 1];
            }
            set
            {
                ControlPoints.Add(value);
            }
        }

        private DxfPoint LastFitPoint
        {
            get
            {
                if (FitPoints.Count == 0) return null;
                return FitPoints[FitPoints.Count - 1];
            }
            set
            {
                FitPoints.Add(value);
            }
        }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 210:
                    Normal.X = double.Parse(value);
                    break;
                case 220:
                    Normal.Y = double.Parse(value);
                    break;
                case 230:
                    Normal.Z = double.Parse(value);
                    break;
                case 70:
                    Flags = (FlagsEnum)Enum.Parse(typeof(FlagsEnum), value);
                    break;
                case 71:
                    Degree = int.Parse(value);
                    break;
                case 72:
                    KnotCount = int.Parse(value);
                    break;
                case 73:
                    ControlPointCount = int.Parse(value);
                    break;
                case 74:
                    FitPointCount = int.Parse(value);
                    break;
                case 42:
                    KnotTolerance = double.Parse(value);
                    break;
                case 43:
                    ControlPointTolerance = double.Parse(value);
                    break;
                case 44:
                    FitPointTolerance = double.Parse(value);
                    break;
                case 12:
                    StartTangent.X = double.Parse(value);
                    break;
                case 22:
                    StartTangent.Y = double.Parse(value);
                    break;
                case 32:
                    StartTangent.Z = double.Parse(value);
                    break;
                case 13:
                    EndTangent.X = double.Parse(value);
                    break;
                case 23:
                    EndTangent.Y = double.Parse(value);
                    break;
                case 33:
                    EndTangent.Z = double.Parse(value);
                    break;
                case 40:
                    KnotValues.Add(double.Parse(value));
                    break;
                case 41:
                    Weight = double.Parse(value);
                    break;
                case 10:
                    if (LastControlPoint == null || LastControlPoint.X != null)
                        LastControlPoint = new DxfPoint();
                    LastControlPoint.X = double.Parse(value);
                    break;
                case 20:
                    if (LastControlPoint == null || LastControlPoint.Y != null)
                        LastControlPoint = new DxfPoint();
                    LastControlPoint.Y = double.Parse(value);
                    break;
                case 30:
                    if (LastControlPoint == null || LastControlPoint.Z != null)
                        LastControlPoint = new DxfPoint();
                    LastControlPoint.Z = double.Parse(value);
                    break;
                case 11:
                    if (LastFitPoint == null || LastFitPoint.X != null)
                        LastFitPoint = new DxfPoint();
                    LastFitPoint.X = double.Parse(value);
                    break;
                case 21:
                    if (LastFitPoint == null || LastFitPoint.Y != null)
                        LastFitPoint = new DxfPoint();
                    LastFitPoint.Y = double.Parse(value);
                    break;
                case 31:
                    if (LastFitPoint == null || LastFitPoint.Z != null)
                        LastFitPoint = new DxfPoint();
                    LastFitPoint.Z = double.Parse(value);
                    break;
            }
        }
    }
}
