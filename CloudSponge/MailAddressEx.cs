using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace CloudSponge
{
    public static class MailAddressEx
    {
        private static Dictionary<string, ContactSource> hostToSourceMapping = new Dictionary<string, ContactSource>(StringComparer.OrdinalIgnoreCase)
            {
                {"gmail.com", ContactSource.Gmail},
                {"live.com", ContactSource.WindowsLive},
                {"yahoo.com", ContactSource.Yahoo},
                {"plaxo.com", ContactSource.Plaxo},
                {"aol.com", ContactSource.AOL}
            };

        public static ContactSource GetCloudSpongeSource(this MailAddress address)
        {
            ContactSource source = ContactSource.Uknown;
            hostToSourceMapping.TryGetValue(address.Host, out source);
            return source;
        }
    }
}
