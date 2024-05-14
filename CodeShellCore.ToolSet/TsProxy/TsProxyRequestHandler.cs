using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using System;
using System.Threading.Tasks;
using CodeShellCore.Proxy;
using CodeShellCore.Text;
using System.IO;
using System.Collections.Generic;

namespace CodeShellCore.ToolSet.TsProxy
{
    public class ProxyFile
    {
        public string Namespace { get; set; }
        public Dictionary<string, PropertyDto> Importations { get; set; } = new Dictionary<string, PropertyDto>();
        public List<string> Entities { get; set; } = new List<string>();
    }
    public class TsProxyRequestHandler : CliRequestHandler<TsProxyRequestDto>
    {
        Dictionary<string, ProxyFile> _files = new Dictionary<string, ProxyFile>();
        public TsProxyRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Generate proxy for ts";

        protected override void Build(ICliRequestBuilder<TsProxyRequestDto> builder)
        {
            builder.FillProperty(e => e.ApiUrl, "url", 'u', 1, true);
            builder.FillProperty(e => e.TargetFolder, "target", 't', 2, true);
        }

        protected override async Task<Result> HandleAsync(TsProxyRequestDto request)
        {
            var serv = new DefaultHttpService(request.ApiUrl);
            var data = await serv.GetAsyncAs<DocumentDto>("api/codeshell/apiDefinition");
            var service = GetService<ITypeScriptGenerationService>();
            var proxyFolder = Path.Combine(request.TargetFolder, "proxy");
            if (!Directory.Exists(proxyFolder))
            {
                Directory.CreateDirectory(proxyFolder);
            }
            Utils.ClearDirectory(proxyFolder);

            foreach (var schema in data.Schemas)
            {
                var ns = schema.Key.GetBeforeLast(".");
                var folder = service.GetFolderPathFromNamespace(ns);
                var file = Utils.CombineUrl(folder, "models.ts");
                if (!_files.ContainsKey(file))
                {
                    _files[file] = new ProxyFile
                    {
                        Namespace = ns
                    };
                }
                var content = service.MapEntity(schema.Value);
                var dic = _files[file].Importations;
                service.ExtractDependencies(schema.Value, ref dic);
                _files[file].Importations = dic;
                _files[file].Entities.Add(content);
            }

            foreach (var file in _files.Keys)
            {
                var content = "";
                var fullPath = Utils.CombineUrl(request.TargetFolder, file);
                content += service.GenerateImportation(_files[file].Importations, _files[file].Namespace) + "\r\n";

                foreach (var entity in _files[file].Entities)
                {
                    content += entity;
                }
                Utils.CreateFolderForFile(fullPath);
                File.WriteAllText(fullPath, content);
            }
            return new Result();
        }
    }
}
