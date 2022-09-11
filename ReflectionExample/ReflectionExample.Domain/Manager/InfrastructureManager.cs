using System.IO;
using System.Net;

namespace ReflectionExample.Domain.Manager
{
    public class InfrastructureManager
    {
        private string url;

        public InfrastructureManager(string url)
        {
            this.url = url;
        }

        public string GetWebSiteStringResponse(string parameterTest)
        {
            WebRequest request = WebRequest.Create(this.url);

            WebResponse response = request.GetResponse();

            string result =
                new StreamReader(response.GetResponseStream()).ReadToEnd();

            return result;
        }
    }
}
