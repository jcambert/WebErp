using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Models
{
    public abstract class ModelBase : IModelBase
    {
        /// <summary>
        /// Id Key Societe + Code
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Societe is first key
        /// </summary>
        public int Societe { get; set; }

        /// <summary>
        /// Code is Second key
        /// </summary>
        public string Code { get; set; }
    }

    [ContractClass(typeof(ModelBaseContract))]
    public interface IModelBase
    {
        string ID { get; set; }
        int Societe { get; set; }
        string Code { get; set; }
    }

    [ContractClassFor(typeof(IModelBase))]
    internal sealed class ModelBaseContract : IModelBase
    {
        public string ID { get; set; }
        public int Societe { get; set; }
        public string Code { get; set; }
    }

}
