using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Http
{
    public class HttpHandler : CliRequestHandler<HttpRequestInput>
    {
        public HttpHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Sends Http Request";

        protected override void Build(ICliRequestBuilder<HttpRequestInput> builder)
        {
            builder.Property(e => e.Method, "method", "m", 1, true).SetOptions(new string[] { "POST", "GET", "PUT", "DELETE" });
            builder.Property(e => e.Url, "url", "u", 2, true);
            builder.Property(e => e.Body, "body", "b", 3).SetDescription("Required of POST or GET");
        }

        protected override async Task<Result> HandleAsync(HttpRequestInput request, CancellationToken token)
        {
            var service = new DefaultHttpService();
            HttpResponseMessage result = null;
            switch (request.Method.ToUpper())
            {
                case "GET":
                    result = await service.GetAsync(request.Url);
                    break;
                case "POST":
                    result = await service.PostStringAsync(request.Url, request.Body);
                    break;
                default:
                    throw new Exception("Invalid method");
            }

            var resultString = await result.Content.ReadAsStringAsync();
            Console.WriteLine((int)result.StatusCode);
            Console.WriteLine(resultString);
            return new Result(0);

        }
    }
}
