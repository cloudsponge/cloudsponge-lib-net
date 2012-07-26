using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CloudSponge
{
    
    public class CloudSpongeResponse
    {
        public int ImportId { get; protected set; }
        public string UserId { get; protected set; }
        public string Echo { get; protected set; }

        public virtual void Populate(XContainer root)
        {
            this.ImportId = int.Parse(root.Element("import-id").Value);
            this.UserId = root.Element("user-id").Value;
            this.Echo = root.Element("echo").Value;
        }
        public virtual void Populate(dynamic json)
        {
            throw new NotImplementedException();
        }
    }
}
