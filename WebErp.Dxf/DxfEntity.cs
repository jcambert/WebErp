using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Entities;
using WebErp.Dxf.Parsers;

namespace WebErp.Dxf
{
    public class DxfEntity
    {
        private readonly List<string> classhierarchy ;
        private readonly List<DxfEntity> children ;
        public DxfEntity()
        {
            classhierarchy = new List<string>();
            children = new List<DxfEntity>();

        }

        [EntityCode(0)]
        public string EntityType { get; set; }
        [EntityCode(5)]
        public string Handle { get; set; }

        public List<string> ClassHierarchy => classhierarchy;
        [EntityCode(67)]
        public bool IsInPaperSpace { get; set; }
        [EntityCode(8)]
        public string LayerName { get; set; }
        [EntityCode(6)]
        public string LineType { get; set; }
        [EntityCode(62)]
        public int ColorNumber { get; set; }
        [EntityCode(48)]
        public double LineTypeScale { get; set; }
        [EntityCode(60)]
        public bool IsInvisible { get; set; }

        public virtual bool HasChildren => children.Count > 0;
        
        public List<DxfEntity> Children => children;

        internal virtual void Parse(int groupcode, string value)
        {
            switch (groupcode)
            {
                case 0:
                    EntityType = value;
                    break;
                case 5:
                    Handle = value;
                    break;
                case 100:
                    ClassHierarchy.Add(value);
                    break;
                case 67:
                    IsInPaperSpace = int.Parse(value) == 1;
                    break;
                case 8:
                    LayerName = value;
                    break;
                case 6:
                    LineType = value;
                    break;
                case 62:
                    ColorNumber = int.Parse(value);
                    break;
                case 48:
                    LineTypeScale = double.Parse(value);
                    break;
                case 60:
                    IsInvisible = int.Parse(value) == 1;
                    break;
            }
        }

        public override string ToString()
        {

            EntitySerializer ser = EntitySerializerProvider.Get(this.GetType());

            return ser.Write(this);
        }
    }
}
