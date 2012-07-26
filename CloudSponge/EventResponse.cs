using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CloudSponge
{
    public enum EventType
    {
        Initializing,
        Gathering,
        Submitting,
        Complete
    }
    public enum EventStatus
    {
        InProgress,
        Completed,
        Error
    }
    public class Event
    {

        public DateTime CreatedAt { get; internal set; }
        public string Description { get; internal set; }
        public EventType Type { get; internal set; }
        public int Id { get; internal set; }
        public int ImportActionId { get; internal set; }
        public EventStatus Status { get; internal set; }
        public bool Unclaimed { get; internal set; }
        public DateTime UpdateAt { get; internal set; }
        public int? Value { get; internal set; }

        internal Event()
        { }
    }
    public class EventsResponse : CloudSpongeResponse
    {
        public IEnumerable<Event> Events { get; private set; }
        public bool IsSuccess
        {
            get
            {
                return (from e in this.Events
                        where e.Type == EventType.Complete && e.Status != EventStatus.InProgress
                        select e).Any();
            }
        }
        public bool IsError
        {
            get
            {
                return (from e in this.Events
                        where e.Status == EventStatus.Error
                        select e).Any();
            }
        }
        public bool IsComplete
        {
            get { return this.IsError || this.IsSuccess; }
        }
        public override void Populate(XContainer root)
        {
            root = root.Element("eventsResponse");

            base.Populate(root);

            Events = from e in root.Element("events").Elements("event")
                     select new Event
                     {
//                         CreatedAt = DateTime.Parse(e.Element("created-at").Value),
                         Description = e.Element("description").Value,
                         Type = (EventType)Enum.Parse(typeof(EventType), e.Element("event-type").Value, true),
//                         Id = int.Parse(e.Element("id").Value),
//                         ImportActionId = int.Parse(e.Element("import-action-id").Value),
                         Status = (EventStatus)Enum.Parse(typeof(EventStatus), e.Element("status").Value, true),
//                         Unclaimed = bool.Parse(e.Element("unclaimed").Value),
//                         UpdateAt = DateTime.Parse(e.Element("updated-at").Value),
                         Value = string.IsNullOrWhiteSpace(e.Element("value").Value) ? null : (int?)int.Parse(e.Element("value").Value)
                     };


        }
    }
}
