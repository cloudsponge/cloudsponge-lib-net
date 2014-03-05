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
                                FirstName = _accessStringProperty(c, "first-name"),
                                LastName = _accessStringProperty(c, "last-name"),
                                EmailAddresses = from e in c.Element("email").Elements("email")
                                                 select _accessStringProperty(e, "address"),
                                PhoneNumbers = from p in c.Element("phone").Elements("phone")
                                               select new PhoneNumber
                                               {
                                                   Number = _accessStringProperty(p, "number"),
                                                   Type = _accessPhoneNumberTypeProperty(p, "type")
                                               }
                            };
            
        }

        private String _accessStringProperty(XElement el, String property)
        {
            String ret = "";
            XElement xValue;
            if (el != null)
            {
                xValue = el.Element(property);
                if (xValue != null)
                {
                    ret = xValue.Value;
                }
            }
            return ret;
        }

        private PhoneNumberType _accessPhoneNumberTypeProperty(XElement el, String property)
        {
            PhoneNumberType ret = PhoneNumberType.Home;
            String phoneTypeString = _accessStringProperty(el, property);
            if (phoneTypeString != "")
            {
                ret = (PhoneNumberType)Enum.Parse(typeof(PhoneNumberType), phoneTypeString, true);
            }
            return ret;
        }
    }
}
