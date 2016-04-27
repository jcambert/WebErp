using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Models.eTole
{
    public class PartTree:ModelBase
    {
        public virtual PartCollection<PartItem> PartItems { get; set; } = new PartCollection<PartItem>();
    }

    public class PartCollection<T> : Collection<T>
    {
        public void Add(ICollection<T> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        [JsonIgnore]
        public string Serialized
        {
            get { return JsonConvert.SerializeObject(this); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                var jData = JsonConvert.DeserializeObject<List<T>>(value);
                this.Items.Clear();
                this.Add(jData);

            }
        }
    }

    public class PartItem
    {
       
        public Guid Id { get; set; }

        public virtual PartCollection<PartItem> PartItems { get; set; } = new PartCollection<PartItem>();

        public virtual PartCollection<PartProperty> PartProperties { get; set; } = new PartCollection<PartProperty>();
    }

    public class PartProperty
    {
        public Guid Id { get; set; }

        public PropertyType PropertyType { get; set; }

        public string StringValue { get; set; }

    }
    public enum PropertyType
    {
        pString,
        pNumeric,
        pBoolean
    }
}
