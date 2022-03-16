using CodeShellCore.Web.Razor.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Services
{
    public class LegacySpaFallbackHandler : ISpaFallbackHandler
    {
        private readonly IRazorRenderingService razor;

        public LegacySpaFallbackHandler(IRazorRenderingService razor)
        {
            this.razor = razor;
        }
        public string DefaultTenant => Shell.GetConfigAs<string>("DefaultTenant", false);

        public async Task HandleRequestAsync(HttpContext con, string defaultTitle = null)
        {
            var legacyHandler = new LegacySpaModelBuilder(con.Request, DefaultTenant);
            var model = legacyHandler.BuildModel(defaultTitle);

            con.Response.ContentType = "text/html";
            var res = razor.RenderPartial(con, "Index", model);

            await con.Response.WriteAsync(res);
        }
    }
}
