using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSponge
{
    public enum ContactSource
    {
        Uknown,
        Yahoo,
        WindowsLive,
        Gmail,
        AOL,
        Plaxo,
        AddressBook,
        Outlook
    }

    public static class ContactSourceEx
    {
        public static AuthenticationMethod GetSupportedMethods(this ContactSource source)
        {
            switch (source)
            {
                case ContactSource.Yahoo:
                case ContactSource.WindowsLive:
                    return AuthenticationMethod.Consent;
                case ContactSource.Gmail:
                    return AuthenticationMethod.Consent;// | AuthenticationMethod.Import;
                case ContactSource.AOL:
                case ContactSource.Plaxo:
                    return AuthenticationMethod.Import;
                case ContactSource.AddressBook:
                case ContactSource.Outlook:
                    return AuthenticationMethod.DesktopApplet;
                case ContactSource.Uknown:
                    return AuthenticationMethod.Consent | AuthenticationMethod.Import | AuthenticationMethod.DesktopApplet;
                default:
                    throw new NotImplementedException();
            }

        }
        public static bool IsSupported(this ContactSource source, AuthenticationMethod authentication)
        {
            return (source.GetSupportedMethods() & authentication) == authentication;
        }
    }
}
