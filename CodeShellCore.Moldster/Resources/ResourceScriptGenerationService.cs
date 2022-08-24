using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.Options;
using System;

namespace CodeShellCore.Moldster.Resources
{
    public class ResourceScriptGenerationService : ScriptGenerationServiceBase, IResourceScriptGenerationService
    {

        public ResourceScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt) : base(prov, opt)
        {
        }



    }
}
