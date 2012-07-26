using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CloudSponge
{
    public enum PhoneNumberType
    {
        Home,
        Work
    }
    public class PhoneNumber
    {
        public PhoneNumberType Type { get; internal set; }
        public string Number { get; internal set; }
    }
    public class Contact
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; internal set; }
        public IEnumerable<string> EmailAddresses { get; internal set; }

    }
    public class ContactsResponse : CloudSpongeResponse
    {
        public IEnumerable<Contact> Contacts { get; private set; }
        public override void Populate(XContainer root)
        {
            root = root.Element("contactsResponse");
            base.Populate(root);

            this.Contacts = from c in root.Element("contacts").Elements("contact")
                            select new Contact
                            {
                                FirstName = c.Element("first-name").Value,
                                LastName = c.Element("last-name").Value,
                                EmailAddresses = from e in c.Element("email").Elements("email")
                                                 select e.Element("address").Value,
                                PhoneNumbers = from p in c.Element("phone").Elements("phone")
                                               select new PhoneNumber
                                               {
                                                   Number = p.Element("number").Value,
                                                   Type = (PhoneNumberType)Enum.Parse(typeof(PhoneNumberType), p.Element("type").Value, true)
                                               }
                            };
            
        }
    }
}
