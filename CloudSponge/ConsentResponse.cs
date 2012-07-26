using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace CloudSponge
{
    public class ConsentResponse : BeginImportResponse
    {
        
        public string Url { get; private set; }

        public override void Populate(XContainer root)
        {
            root = root.Element("result");

            base.Populate(root);
            
            Url = root.Element("url").Value;
        }
    }
}
