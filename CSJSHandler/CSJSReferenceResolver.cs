using RazorEngine.Compilation;
using RazorEngine.Compilation.ReferenceResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSJSHandler
{
    public class CSJSReferenceResolver : IReferenceResolver
    {
        public string FindLoaded(IEnumerable<string> refs, string find)
        {
            return refs.First(r => r.EndsWith(System.IO.Path.DirectorySeparatorChar + find));
        }

        public IEnumerable<CompilerReference> GetReferences(TypeContext context, IEnumerable<CompilerReference> includeAssemblies)
        {
            IEnumerable<string> loadedAssemblies = (new UseCurrentAssembliesReferenceResolver())
                .GetReferences(context, includeAssemblies)
                .Select(r => r.GetFile())
                .ToArray();

            // If more assembly references are required for RazorEngine then add references here also
            // so RazorEngine will load the assemblies for its compiler
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "mscorlib.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "System.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "System.Core.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "System.Web.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "System.Web.Mvc.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "System.Web.Razor.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "RazorEngine.dll"));
            yield return CompilerReference.From(FindLoaded(loadedAssemblies, "CSJSHandler.dll"));
        }
    }
}
