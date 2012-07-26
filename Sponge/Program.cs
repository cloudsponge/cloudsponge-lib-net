using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudSponge;
using Sponge.Properties;
using System.Diagnostics;

namespace Sponge
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new Api(Settings.Default.DomainKey, Settings.Default.DomainPassword);

            var consent = api.Consent(ContactSource.Gmail);

            Process.Start(consent.Url);


            bool complete = false;
            while (!complete)
            {
                var events = api.Events(consent.ImportId);

                foreach (var item in events.Events)
                    Console.WriteLine("{0}-{1}", item.Type, item.Status);

                complete = events.IsComplete;
            }

            var contacts = api.Contacts(consent.ImportId);

            foreach (var item in contacts.Contacts)
                Console.WriteLine("{1}, {0} ({2})", item.FirstName, item.LastName, item.EmailAddresses.FirstOrDefault());
        }
    }
}
