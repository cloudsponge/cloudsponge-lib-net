using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CloudSponge
{
    public class ImportResponse: BeginImportResponse
    {
        public override void Populate(XContainer root)
        {
            root = root.Element("result");
            base.Populate(root);
        }
    }
}
