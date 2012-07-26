using CloudSponge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;

namespace CloudSponge.Test
{


    /// <summary>
    ///This is a test class for ConsentResponseTest and is intended
    ///to contain all ConsentResponseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConsentResponseTest
    {

        /// <summary>
        ///A test for Populate
        ///</summary>
        [TestMethod()]
        public void PopulateTest()
        {
            ConsentResponse target = new ConsentResponse(); // TODO: Initialize to an appropriate value
            XContainer root = new XElement("root",
                new XElement("result",
                    new XElement("url", "ABC"),
                    new XElement("import-id", 1),
                    new XElement("user-id", "ABC"),
                    new XElement("echo", "123")
                    )
                );
            target.Populate(root);

            Assert.AreEqual("ABC", target.Url);
        }
    }
}
