using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Data.Extensions
{
    public static class NinjectExtensions
    {
        private static void BindAllInterfaces<T>(this IKernel kernel, params Type[] types)
        {
            Action<IFromSyntax> a = x => { x.FromAssemblyContaining(types).SelectAllClasses().InheritedFrom<T>().BindAllInterfaces(); };
            kernel.Bind(a);
        }
    }
}
