using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Models
{
    public abstract class ModelBase :ReactiveObject, IModelBase
    {
        private int _societe;
        private string _code;
        /// <summary>
        /// Id Key Societe + Code
        /// </summary>
        public string ID
        {
            get
            {
                return string.Format("{0}-{1}", _societe.ToString(), _code);
            }
            set
            {
                _societe = Int32.Parse( value.Split('-')[0]);
                _code = value.Split('-')[1];
            }
        }

        /// <summary>
        /// Societe is first key
        /// </summary>
        public int Societe { get { return _societe; } set { _societe = value; } }

        /// <summary>
        /// Code is Second key
        /// </summary>
        public string Code { get { return _code; } set { _code = value; } }
    }

    [ContractClass(typeof(ModelBaseContract))]
    public interface IModelBase
    {
        string ID { get; set; }
        int Societe { get; set; }
        string Code { get; set; }
    }

    [ContractClassFor(typeof(IModelBase))]
    internal abstract class ModelBaseContract : IModelBase
    {
        public string ID { get; set; }
        public int Societe { get; set; }
        public string Code { get; set; }
    }

}
