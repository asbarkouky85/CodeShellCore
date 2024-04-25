using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.ToolSet.TsProxy.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.TsProxy
{
    public class TsProxyRequestHandler : CliRequestHandler<TsProxyRequestDto>
    {
        public TsProxyRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Generate proxy for ts";

        protected override void Build(ICliRequestBuilder<TsProxyRequestDto> builder)
        {
            builder.FillProperty(e => e.SwaggerUrl, "url", 'u', 1, true);
        }

        protected override async Task<Result> HandleAsync(TsProxyRequestDto request)
        {
            var serv = new DefaultHttpService(request.SwaggerUrl);
            var data = await serv.GetAsyncAs<SwaggerDocumentDto>("/swagger/v1/swagger.json");

            return new Result();
        }
    }
}
