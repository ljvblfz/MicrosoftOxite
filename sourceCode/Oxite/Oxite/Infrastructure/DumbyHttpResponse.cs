using System.Web;

namespace Oxite.Infrastructure
{
    public class DummyHttpResponse : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}