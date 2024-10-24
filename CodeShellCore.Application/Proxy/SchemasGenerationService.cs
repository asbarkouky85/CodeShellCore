using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeShellCore.Proxy
{
    public class SchemasGenerationService : ApplicationService, ISchemasGenerationService
    {
        private readonly ITypeScriptGenerationService _service;
        public SchemasGenerationService(IServiceProvider provider, ITypeScriptGenerationService service) : base(provider)
        {
            _service = service;
        }

        public async Task GenerateProxyServices(Dictionary<string, GroupDto> modules, string targetFolder)
        {
            var proxyFiles = new Dictionary<string, ProxyServiceFile>();
            foreach (var module in modules)
            {
                foreach (var pair in module.Value.Services)
                {
                    var folder = _service.GetFolderPathFromNamespace(pair.Value.Namespace);
                    var path = Utils.CombineUrl(folder, _service.ApplyNamingConvension(pair.Key) + ".service.ts");
                    if (!proxyFiles.TryGetValue(path, out ProxyServiceFile data))
                    {
                        data = new ProxyServiceFile { Namespace = pair.Value.Namespace };
                        proxyFiles[path] = data;
                    }
                    data.Content = _service.MapService(pair.Key, pair.Value);
                    data.Importations = _service.ExtractDependencies(pair.Value, new Dictionary<string, PropertyDto>());
                }
            }
            await WriteServiceFiles(targetFolder, proxyFiles);
        }

        public async Task GenerateSchemas(Dictionary<string, SchemaItemDto> schemas, string targetFolder)
        {
            var proxyFiles = new Dictionary<string, ProxyModelsFile>();
            foreach (var schema in schemas)
            {
                var ns = schema.Key.GetBeforeLast(".");
                var folder = _service.GetFolderPathFromNamespace(ns);
                var file = Utils.CombineUrl(folder, "models.ts");
                if (!proxyFiles.ContainsKey(file))
                {
                    proxyFiles[file] = new ProxyModelsFile
                    {
                        Namespace = ns
                    };
                }
                var content = _service.MapEntity(schema.Value);

                proxyFiles[file].Importations = _service.ExtractDependencies(schema.Value, proxyFiles[file].Importations);
                proxyFiles[file].Entities.Add(content);
            }
            await WriteModelFiles(targetFolder, proxyFiles);
        }

        private string _generateModelsFile(ProxyModelsFile file)
        {
            var content = "";

            content += _service.GenerateImportation(file.Importations, file.Namespace) + "\r\n";

            foreach (var entity in file.Entities)
            {
                content += entity;
            }
            return content;
        }

        private string _generateServiceFile(ProxyServiceFile file)
        {
            var content = _defaultServiceImportations();
            content += _service.GenerateImportation(file.Importations) + "\r\n";
            content += file.Content;
            return content;
        }

        private string _defaultServiceImportations()
        {
            return @"import { Injectable } from ""@angular/core"";
import { CodeShellProxyService } from ""codeshell/http"";
import { Observable } from ""rxjs"";
";
        }

        public void ClearProxyFolder(string targetFolder)
        {
            var proxyFolder = Path.Combine(targetFolder, "proxy");
            if (!Directory.Exists(proxyFolder))
            {
                Directory.CreateDirectory(proxyFolder);
            }
            Utils.ClearDirectory(proxyFolder);

        }

        public async Task WriteModelFiles(string targetFolder, Dictionary<string, ProxyModelsFile> files)
        {
            foreach (var pair in files)
            {
                var content = _generateModelsFile(pair.Value);
                var fullPath = Utils.CombineUrl(targetFolder, pair.Key);
                Utils.CreateFolderForFile(fullPath);
                await File.WriteAllTextAsync(fullPath, content);
            }
        }

        public async Task WriteServiceFiles(string targetFolder, Dictionary<string, ProxyServiceFile> files)
        {
            foreach (var pair in files)
            {
                var content = _generateServiceFile(pair.Value);
                var fullPath = Utils.CombineUrl(targetFolder, pair.Key);
                Utils.CreateFolderForFile(fullPath);
                await File.WriteAllTextAsync(fullPath, content);
            }
        }
    }
}
