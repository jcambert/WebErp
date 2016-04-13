using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    internal class DxfLineTypeRecord:DxfRecord
    {
        public string LineTypeName { get; set; }
        public string Description { get; set; }
        public int AlignmentCode { get; set; }

        [Flags]
        public enum ElementFlags
        {
            None = 0,
            AbsoluteRotation = 1,
            IsString = 2,
            IsShape = 4
        }

        public class LineTypeElement
        {
            public double Length { get; set; }
            public ElementFlags Flags { get; set; }
            public int? ShapeNumber { get; set; }
            public string Shape { get; set; }
            private List<double> scalings = new List<double>();
            public List<double> Scalings { get { return scalings; } }
            public double? Rotation { get; set; }
            private List<double> xoffsets = new List<double>();
            public List<double> XOffsets { get { return xoffsets; } }
            private List<double> yoffsets = new List<double>();
            public List<double> YOffsets { get { return yoffsets; } }

            public string Text { get; set; }
        }

        private List<LineTypeElement> elements = new List<LineTypeElement>();
        public List<LineTypeElement> Elements { get { return elements; } }
        public int ElementCount { get; set; }
        public double PatternLength { get; set; }
    }
}
