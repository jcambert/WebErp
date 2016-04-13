using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers.Attributes;

namespace WebErp.Dxf.Entities
{
    internal class DxfGenericEntity : DxfEntity
    {
        public class Entry
        {
            public int GroupCode { get; set; }
            public string Value { get; set; }
            public Entry()
            {
            }

            public Entry(int g, string v)
            {
                GroupCode = g;
                Value = v;
            }
        }

        private List<Entry> entries = new List<Entry>();
        public List<Entry> Entries { get { return entries; } }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            Entries.Add(new Entry(groupcode, value));
        }
    }

    [Entity("3DSOLID")]
    internal class DXF3DSolid : DxfGenericEntity
    {
    }

    [Entity("ACAD_PROXY_ENTITY")]
    internal class DXF3DAcadProxy : DxfGenericEntity
    {
    }

    [Entity("ATTDEF")]
    internal class DXFAttributeDefinition : DxfGenericEntity
    {
    }

    [Entity("ATTRIB")]
    internal class DXFAttribute : DxfGenericEntity
    {
    }

    [Entity("BODY")]
    internal class DXFBody : DxfGenericEntity
    {
    }

    [Entity("DIMENSION")]
    internal class DXFDimension : DxfGenericEntity
    {
    }

    [Entity("HATCH")]
    internal class DXFHatch : DxfGenericEntity
    {
    }

    [Entity("IMAGE")]
    internal class DXFImage : DxfGenericEntity
    {
    }

    [Entity("LEADER")]
    internal class DXFLeader : DxfGenericEntity
    {
    }

    [Entity("MLINE")]
    internal class DXFMLine : DxfGenericEntity
    {
    }

    [Entity("MTEXT")]
    internal class DXFMText : DxfGenericEntity
    {
    }

    [Entity("OLEFRAME")]
    internal class DXFOleFrame : DxfGenericEntity
    {
    }

    [Entity("OLE2FRAME")]
    internal class DXFOle2Frame : DxfGenericEntity
    {
    }

    [Entity("REGION")]
    internal class DXFRegion : DxfGenericEntity
    {
    }

    [Entity("TEXT")]
    internal class DXFText : DxfGenericEntity
    {
    }

    [Entity("VIEWPORT")]
    internal class DXFViewPort : DxfGenericEntity
    {
    }
}
