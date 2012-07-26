using CloudSponge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;

namespace CloudSponge.Test
{
    /// <summary>
    ///This is a test class for CloudSpongeResponseTest and is intended
    ///to contain all CloudSpongeResponseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CloudSpongeResponseTest
    {
        /// <summary>
        ///A test for Populate
        ///</summary>
        [TestMethod()]
        public void PopulateXmlTest()
        {
            CloudSpongeResponse target = new CloudSpongeResponse();
            XContainer root = new XElement("root");
            AddElements(root);
            target.Populate(root);

            Assert.AreEqual(1, target.ImportId);
            Assert.AreEqual("ABC", target.UserId);
            Assert.AreEqual("123", target.Echo);
        }

        protected virtual void AddElements(XContainer root)
        {
            root.Add(new XElement("import-id", 1));
            root.Add(new XElement("user-id", "ABC"));
            root.Add(new XElement("echo", "123"));
        }
    }
}
