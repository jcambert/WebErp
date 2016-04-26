using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("CIRCLE")]
    public class DxfCircle : DxfEntity
    {

        Dictionary<int, Expression<Func<DxfCircle>>> maps = new Dictionary<int, Expression<Func<DxfCircle>>>();
        private readonly DxfPoint center;
        private readonly DxfPoint extrusion;

        public DxfCircle()
        {
            center = new DxfPoint();
            extrusion = new DxfPoint();
        }

        public double Thickness { get; set; }

        [EntityCode(10, "X")]
        [EntityCode(20, "Y")]
        [EntityCode(30, "Z")]
        public DxfPoint Center => center;

        [EntityCode(40)]
        public double Radius { get; set; }

        [EntityCode(210,"X")]
        [EntityCode(220,"Y")]
        [EntityCode(230,"Z")]
        public DxfPoint ExtrusionDirection => extrusion;

        
       
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

            //SetPropertyFromDbValue<DxfCircle,double?>(this, o=>ExtrusionDirection.X,double.Parse(value));
            
        }

        


    }

    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true,Inherited = true)]
    public class EntityCodeAttribute : Attribute
    {
        private readonly int _code;
        private readonly string _propertyName;

        public EntityCodeAttribute(int code,string propertyName="")
        {
            this._code = code;
            this._propertyName = propertyName;
        }

        public int Code => _code;

        public string PropertyName => _propertyName;
    }
  
    
}
