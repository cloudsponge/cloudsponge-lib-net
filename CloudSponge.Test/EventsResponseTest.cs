using CloudSponge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using System.Linq;


namespace CloudSponge.Test
{
    /// <summary>
    ///This is a test class for CloudSpongeResponseTest and is intended
    ///to contain all CloudSpongeResponseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventsResponseTest
    {
        /// <summary>
        ///A test for Populate
        ///</summary>
        [TestMethod()]
        public void PopulateXmlTest()
        {
            EventsResponse target = new EventsResponse();
            XContainer root = new XElement("root");
            XContainer eventsResponse = new XElement("eventsResponse");
            AddElements(eventsResponse);
            root.Add(eventsResponse);
            target.Populate(root);

            Assert.AreEqual(1, target.ImportId);
            Assert.AreEqual("ABC", target.UserId);
            Assert.AreEqual("123", target.Echo);

            Assert.AreEqual(false, target.IsComplete);
            Assert.AreEqual(false, target.IsError);
            Assert.AreEqual(true, (from e in target.Events
                                                  where e.Type == EventType.Submitting
                                                  select e).Any());
        }

        protected virtual void AddElements(XContainer root)
        {
            XContainer events = new XElement("events");
            XContainer eventEntity = new XElement("event");
            root.Add(new XElement("import-id", 1));
            root.Add(new XElement("user-id", "ABC"));
            root.Add(new XElement("echo", "123"));

            eventEntity.Add(new XElement("description", "This is a test response"));
            eventEntity.Add(new XElement("event-type", "SUBMITTING"));
            eventEntity.Add(new XElement("status", "INPROGRESS"));
            eventEntity.Add(new XElement("value", "50"));

            events.Add(eventEntity);
            root.Add(events);
        }
    }
}
