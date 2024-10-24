using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Proxy;
using System;
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
            builder.Property(e => e.ApiUrl, "url", "u", 1, true);
            builder.Property(e => e.TargetFolder, "target", "t", 2, true);
        }

        protected override async Task<Result> HandleAsync(TsProxyRequestDto request)
        {
            var serv = new DefaultHttpService(request.ApiUrl);
            var data = await serv.GetAsyncAs<DocumentDto>("api/codeshell/apiDefinition");
            var service = GetService<ISchemasGenerationService>();

            service.ClearProxyFolder(request.TargetFolder);
            await service.GenerateSchemas(data.Schemas, request.TargetFolder);
            await service.GenerateProxyServices(data.Modules, request.TargetFolder);
            return new Result();
        }
    }
}
