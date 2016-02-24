using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Extensions.Conventions;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;

namespace WebErp.Some.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var kernel = new StandardKernel();
            Assert.IsNotNull(kernel);

            kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom(typeof(IConfiguration)).BindAllInterfaces());

            kernel.Get(typeof(IConfiguration<int>)).Should().NotBeNull();

            var all = kernel.GetAll(typeof(IConfiguration)).ToList();
            all.Should().NotBeEmpty();
            all.ForEach(a=> { ((IConfiguration)a).ConfigureModel(""); });

        }
    }
}
