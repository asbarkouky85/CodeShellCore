using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.Resources.Services
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
