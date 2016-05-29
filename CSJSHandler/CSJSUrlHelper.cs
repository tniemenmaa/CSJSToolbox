using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace CSJSHandler
{
    public class CSJSUrlHelper
    {
        private string BaseUrl { get; set; }
        public CSJSUrlHelper()
        {
            // TODO: Add handling for ApplicationPath inside IIS, currently only works if the application resides in the base.
            var route = RouteTable.Routes.FirstOrDefault(x => x.GetType() == typeof(System.Web.Routing.Route)) as Route;
            BaseUrl = route.Url.Replace("{controller}", "{0}").Replace("{action}", "{1}").Replace("{id}", "{2}");
        }

        // TODO: Overloads for different Action methods
        public string Action(string action, string controller)
        {
            return string.Format(BaseUrl, controller, action, "");
        }
    }
}
