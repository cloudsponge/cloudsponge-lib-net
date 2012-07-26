using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CloudSponge
{
    public interface IRequestProcessor
    {
        Stream Process(string uri, string key, string password, out Format format);
    }
}
