using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebErp.Dxf.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DxfDocument doc = DxfDocument.Load(@"Q:\dxf\etagere-renfort-poub.dxf");
            Assert.IsNotNull(doc);
        }
    }
}
