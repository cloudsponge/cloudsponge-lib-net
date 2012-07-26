using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CloudSponge
{
    public class CloudSpongeException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        private CloudSpongeException(HttpStatusCode status, string message)
            : base(message)
        {
            this.StatusCode = status;
        }

        internal static Exception FromHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            string message;
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    message = "Invalid parameters were supplied";
                    break;
                case HttpStatusCode.Unauthorized:
                    message = "Access was denied to the requested resource. Either the supplied domain_key and domain_password did not match a domain or the request attempted to access a resource that you don’t have access to.";
                    break;
                case HttpStatusCode.NotFound:
                    message = "The contacts are not yet available.";
                    break;
                case HttpStatusCode.Gone:
                    message = "The contacts have already been retrieved and have been deleted from CloudSponge.com.";
                    break;
                default:
                    message = "An unkown error occured.";
                    break;
            }

            throw new CloudSpongeException(httpStatusCode, message);
        }
    }
}
