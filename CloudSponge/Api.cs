using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Net.Mail;

namespace CloudSponge
{
    public enum Format
    {
        Xml,
        JSON
    }
    public class Api
    {
        const string Host = "https://api.cloudsponge.com/";
        const string Format = "xml";

        static readonly string ContactsRoot = string.Format("{0}contacts.{1}/", Host, Format);
        static readonly string ContactsFormat = string.Format("{0}{{0}}?echo={{1}}", ContactsRoot);

        static readonly string BeginImportRoot = string.Format("{0}begin_import/", Host);

        static readonly string EventsRoot = string.Format("{0}events.{1}/", Host, Format);
        static readonly string EventsFormat = string.Format("{0}{{0}}?echo={{1}}", EventsRoot);

        static readonly string ConsentRoot = string.Format("{0}user_consent.{1}", BeginImportRoot, Format);
        static readonly string ConsentFormat = string.Format("{0}?service={{0}}&user_id={{1}}&echo={{2}}", ConsentRoot);

        static readonly string DesktopRoot = string.Format("{0}desktop_applet.{1}", BeginImportRoot, Format);
        static readonly string DesktopFormat = string.Format("{0}?service={{0}}&user_id={{1}}&echo={{2}}", DesktopRoot);

        static readonly string ImportRoot = string.Format("{0}user_consent.{1}", BeginImportRoot, Format);
        static readonly string ImportFormat = string.Format("{0}?service={{2}}&username={{0}}&password={{1}}&user_id={{3}}&echo={{4}}", ImportRoot);


        public string DomainKey { get; private set; }
        public string DomainPassword { get; private set; }

        public Api(string domainKey, string domainPassword)
        {
            this.DomainKey = domainKey;
            this.DomainPassword = domainPassword;
        }
        private T GetResponse<T>(string uri) where T : CloudSpongeResponse, new()
        {

            var request = (HttpWebRequest)HttpWebRequest.Create(uri);
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", DomainKey, DomainPassword)));

            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", encoded);

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw CloudSpongeException.FromHttpStatusCode(response.StatusCode);

                    T t = new T();
                    using (var stream = response.GetResponseStream())
                    {
                        var document = XDocument.Load(stream);

                        t.Populate(document);

                        return t;
                    }
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                    throw CloudSpongeException.FromHttpStatusCode(((HttpWebResponse)wex.Response).StatusCode);
                throw;
            }


        }
        public ImportResponse Import(string username, string password, ContactSource service, string userId = "", string echo = "")
        {
            string uri = string.Format(ImportFormat, username, password, service, userId, echo);
            return GetResponse<ImportResponse>(uri);
        }
        public ContactsResponse Contacts(int importId, string echo = "")
        {
            string uri = string.Format(ContactsFormat, importId, echo);
            return GetResponse<ContactsResponse>(uri);
        }
        public EventsResponse Events(int importId, string echo = "")
        {
            string uri = string.Format(EventsFormat, importId, echo);
            return GetResponse<EventsResponse>(uri);
        }
        public ConsentResponse Consent(MailAddress address, string userId = "", string echo = "")
        {
            ContactSource source = address.GetCloudSpongeSource();

            if (source != ContactSource.Uknown)
                return Consent(source, userId, echo);
            else
                return Consent(address.Host, userId, echo);
        }
        public ConsentResponse Consent(string service, string userId = "", string echo = "")
        {
            string uri = string.Format(ConsentFormat, service, userId, echo);
            return GetResponse<ConsentResponse>(uri);
        }
        public ConsentResponse Consent(ContactSource service, string userId = "", string echo = "")
        {
            return Consent(service.ToString(), userId, echo);
        }
        public ConsentResponse Desktop(string service, string userId = "", string echo = "")
        {
          string uri = string.Format(DesktopFormat, service, userId, echo);
          return GetResponse<ConsentResponse>(uri);
        }
        public ConsentResponse Desktop(ContactSource service, string userId = "", string echo = "")
        {
          return Desktop(service.ToString(), userId, echo);
        }
    }
}
