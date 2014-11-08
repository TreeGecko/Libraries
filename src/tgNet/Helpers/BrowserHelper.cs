using System.Text;
using System.Web;

namespace TreeGecko.Library.Net.Helpers
{
    public static class BrowserHelper
    {
        public static string GetBrowserType(HttpRequest _request)
        {
            if (_request != null)
            {
                HttpBrowserCapabilities browser = _request.Browser;

                if (browser != null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendFormat("Type = {0}; ", browser.Type);
                    sb.AppendFormat("Name = {0}; ", browser.Browser);
                    sb.AppendFormat("Version = {0}; ", browser.Version);
                    sb.AppendFormat("Platform = {0}; ", browser.Platform);
                    
                    return sb.ToString();
                }
            }

            return "unknown";
        }
    }
}
