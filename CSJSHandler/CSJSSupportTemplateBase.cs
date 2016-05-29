using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CSJSHandler
{
    public class CSJSSupportTemplateBase<T> : TemplateBase<T>
    {
        public CSJSSupportTemplateBase() {
            Url = new CSJSUrlHelper();
        }

        // Custom Url helper since ASP.NET UrlHelper was not functional outside ASP.NET environment
        // If more helpers are needed for RazorEngine then add them here
        public CSJSUrlHelper Url { get; set; }
    }
}
