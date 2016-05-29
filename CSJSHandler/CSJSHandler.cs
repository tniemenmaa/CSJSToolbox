using System.CodeDom.Compiler;
using System.Web;
using System;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;
using System.Text;
using RazorEngine.Configuration;
using System.Collections.Generic;

namespace CSJSHandler
{
    public class CSJSHandler : IHttpHandler
    {
        private ITemplateServiceConfiguration config = null; 
        private IRazorEngineService service = null;
        
        public CSJSHandler()
        {
            config = new TemplateServiceConfiguration { 
                ReferenceResolver = new CSJSReferenceResolver(),
                BaseTemplateType = typeof(CSJSSupportTemplateBase<>)
            };
            
            service = RazorEngineService.Create(config);
        }

        public bool IsReusable
        {  
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var stringBuilder = new StringBuilder();
            var path = context.Request.PhysicalPath;

            using (var fs = File.Open(path, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    string csjsFile = sr.ReadToEnd();

                    // Compile the .csjs file into javascript using RazorEngine
                    var result = service.RunCompile(csjsFile, csjsFile, null, HttpContext.Current.Request.QueryString);
                    context.Response.Write(result);
                }
            }
            
            
        }
    }
}
