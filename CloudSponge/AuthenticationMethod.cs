using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSponge
{
    [Flags]
    public enum AuthenticationMethod: byte
    {
        Consent = 1,
        Import = 2,
        DesktopApplet = 4
    }
}
