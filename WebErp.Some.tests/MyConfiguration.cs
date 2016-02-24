using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Some.tests
{
    public interface IConfiguration
    {
        void ConfigureModel(string s);
    }
    public interface IConfiguration<T>:IConfiguration
    {

    }

    public abstract class Configuration<T> : IConfiguration<T>
    {
        public Configuration()
        {

        }
        public virtual void ConfigureModel(string s)
        {
            Debug.WriteLine("Configuration:" + s);
        }
    }
    public class IntConfiguration :Configuration<int>
    {
        public IntConfiguration()
        {

        }
        public override void ConfigureModel(string s)
        {
            base.ConfigureModel("int:"+s);
        }
    }

    public class DoubleConfiguration : Configuration<double>
    {
        public DoubleConfiguration()
        {

        }
        public override void ConfigureModel(string s)
        {
            base.ConfigureModel("double:"+s);
        }
    }
}
