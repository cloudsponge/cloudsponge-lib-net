using CloudSponge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;

namespace CloudSponge.Test
{
    
    
    /// <summary>
    ///This is a test class for BeginImportResponseTest and is intended
    ///to contain all BeginImportResponseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BeginImportResponseTest : CloudSpongeResponseTest
    {
        /// <summary>
        ///A test for Populate
        ///</summary>
        [TestMethod()]
        public void PopulateXmlTest()
        {
            BeginImportResponse target = new BeginImportResponse(); // TODO: Initialize to an appropriate value
            XContainer root = new XElement("root");
            AddElements(root);
            target.Populate(root);

            Assert.AreEqual(true, target.Success);
        }
        protected override void AddElements(XContainer root)
        {
            base.AddElements(root);
            root.Add(new XElement("status", "success"));
        }
    }
}
