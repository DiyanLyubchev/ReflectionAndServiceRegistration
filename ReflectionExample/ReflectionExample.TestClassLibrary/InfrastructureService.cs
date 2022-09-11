using System.IO;
using System.Net;

namespace ReflectionExample.TestClassLibrary
{
    public class InfrastructureService
    {
        private string url;

        public InfrastructureService(string url)
        {
            this.url = url;
        }

        public string GetWebSiteStringResponse()
        {
            WebRequest request = WebRequest.Create(this.url);

            WebResponse response = request.GetResponse();

            string result =
                new StreamReader(response.GetResponseStream()).ReadToEnd();

            return result;
        }
    }
}
