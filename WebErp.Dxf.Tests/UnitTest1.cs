using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebErp.Dxf.Entities;
using System.Threading.Tasks;
using System.IO;

namespace WebErp.Dxf.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //string _filename = @"Q:\dxf\etagere-renfort-poub{0}.dxf";
            string _filename = @"C:\Users\jc\Documents\test1{0}.dxf";
            string filename = String.Format(_filename, "");
            string copy = String.Format(_filename, "-copy");

            DxfDocument doc = DxfDocument.Load(filename);
            Assert.IsNotNull(doc);

            await doc.SaveAs(copy);

            DxfDocument doc_copy = DxfDocument.Load(copy);
            Assert.IsNotNull(doc_copy);
        }

        [TestMethod]
        public async Task MyTestMethod()
        {


            using (StreamWriter writer = File.CreateText(@"q:\dxf\newfile.txt"))
            {
                await writer.WriteLineAsync("First line of example");
                await writer.WriteLineAsync("and second line");
            }

        }
    }
}
