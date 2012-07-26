using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CloudSponge
{
    public class BeginImportResponse: CloudSpongeResponse
    {
        public bool Success { get; private set; }
        public override void Populate(XContainer root)
        {
            base.Populate(root);
            
            Success = root.Element("status").Value == "success";
        }
    }
}
