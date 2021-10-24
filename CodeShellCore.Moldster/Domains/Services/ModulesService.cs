using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Domains.Dtos;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace CodeShellCore.Moldster.Domains.Services
{
    public class ModulesService : MoldsterFileHandlingService, IModulesService
    {
        static string tmpLocation;
        private IConfigUnit unit => GetService<IConfigUnit>();

        static ModulesService()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            tmpLocation = Path.Combine(docs, "modulesTmp");
            if (!Directory.Exists(tmpLocation))
                Directory.CreateDirectory(tmpLocation);
        }
        public ModulesService(IServiceProvider prov) : base(prov)
        {
        }

        public byte[] GetHtmlArchive(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var manager = new ResourceManager($"{assemblyName}.Properties.Resources", assembly);
            object obj = manager.GetObject("Html", new System.Globalization.CultureInfo("en"));
            return (byte[])obj;
        }

        public byte[] GetScriptArchive(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var manager = new ResourceManager($"{assemblyName}.Properties.Resources", assembly);
            object obj = manager.GetObject("Scripts", new System.Globalization.CultureInfo("en"));
            return (byte[])obj;
        }

        public virtual Result InstallModule(string assemblyName, string toPath = null)
        {
            var mod = MoldsterModulesConfig.Modules.Where(d => d.AssemblyName == assemblyName).FirstOrDefault();
            if (mod == null)
                return new Result { Code = 1, Message = $"Module not registered '{assemblyName}'" };

            mod.Categories = mod.Categories ?? new List<ModuleCategoryDTO>();
            mod.Resources = mod.Resources ?? new string[0];
            toPath = (toPath ?? mod.InstallPath).ToLower();

            List<string> resNames = new List<string>();
            resNames.AddRange(mod.Resources);
            //resNames.AddRange(mod.Categories.Where(d => d.Resource != null).Select(d => d.Resource).ToList());
            resNames = resNames.Distinct().ToList();

            List<Resource> res = new List<Resource>();
            Dictionary<string, Domain> dic = new Dictionary<string, Domain>();
            List<Domain> createdDomains = new List<Domain>();
            foreach (var r in resNames)
            {
                string domain = null;
                string resourceName = r;
                if (r.Contains(":"))
                {
                    var spl = r.Split(':');
                    domain = spl[1];
                    resourceName = spl[0];
                }

                var rs = unit.ResourceRepository.GetResource(resourceName, domain, createdDomains);
                res.Add(rs);


            }

            Utils.ClearDirectory(tmpLocation);

            var scrPath = Path.Combine(tmpLocation, mod.AssemblyName + ".scripts");
            var scrArch = GetScriptArchive(mod.AssemblyName);
            var scripts = DecompressFolder(scrArch, scrPath, "*.ts");

            var vwsPath = Path.Combine(tmpLocation, mod.AssemblyName + ".views");
            var vwsArch = GetHtmlArchive(mod.AssemblyName);
            var views = DecompressFolder(vwsArch, vwsPath, "*.cshtml");

            List<Tuple<string, string>> files = new List<Tuple<string, string>>();

            foreach (var view in views)
            {
                string viewPath = view.Replace(vwsPath + "\\", "").Replace("\\", "/").Replace(".cshtml", "");

                var conf = mod.Categories.Where(d => d.Path == viewPath).FirstOrDefault();
                if (conf != null)
                {
                    PageCategory cat = new PageCategory();
                    Resource rs = null;

                    if (!string.IsNullOrEmpty(conf.Resource))
                        rs = res.FirstOrDefault(d => d.Name == conf.Resource);
                    cat.BaseComponent = conf.Base;
                    cat.ViewPath = Utils.CombineUrl(toPath, viewPath);
                    Out.Write($"Adding template '{cat.ViewPath}'");

                    if (unit.PageCategoryRepository.Exist(d => d.ViewPath == cat.ViewPath))
                    {
                        Out.Write("Already Exists");

                    }
                    else
                    {
                        string domPath = cat.ViewPath.GetBeforeLast("/");
                        Domain dom = null;
                        if (!dic.TryGetValue(domPath, out dom))
                        {
                            dom = unit.DomainRepository.GetOrCreatePath(domPath, ref createdDomains);
                            dic[domPath] = dom;
                        }
                        unit.PageCategoryRepository.Add(cat, dom, rs);
                        WriteSuccess();
                    }
                    Out.WriteLine();
                }

                string htmlPath = Path.Combine(Paths.ConfigRoot, "Views", toPath, viewPath + ".cshtml");
                files.Add(new Tuple<string, string>(view, htmlPath));
            }

            foreach (var s in scripts)
            {
                string scriptName = s.Replace(scrPath + "\\", "");
                string scriptPath = Path.Combine(Names.BaseFolder, toPath, scriptName);
                files.Add(new Tuple<string, string>(s, scriptPath));
            }

            var resp = unit.SaveChanges();

            if (resp.IsSuccess)
            {
                Out.WriteLine("Module Added to Database [Affected : " + resp.AffectedRows + "]");
                foreach (var f in files)
                {
                    WriteFileOperation("Adding", Path.GetFileName(f.Item2), false);
                    if (File.Exists(f.Item2))
                    {
                        GotoColumn(SuccessCol);
                        Out.Write("Already Exists");
                    }
                    else
                    {
                        Utils.CreateFolderForFile(f.Item2);
                        File.Move(f.Item1, f.Item2);
                        WriteSuccess();
                    }
                    Out.WriteLine();
                }
            }

            Utils.ClearDirectory(tmpLocation);

            return resp;
        }

        string[] DecompressFolder(byte[] zipFile, string arch, string pattern)
        {
            Utils.CreateFolderForFile(arch);
            File.WriteAllBytes(arch + ".zip", zipFile);
            FileUtils.DecompressDirectory(arch + ".zip", arch);

            return Directory.GetFiles(arch, pattern, SearchOption.AllDirectories);
        }

        byte[] CompressFolder(string fold, string name)
        {
            var arch = Path.Combine(tmpLocation, name) + ".zip";
            FileUtils.CompressDirectory(fold, arch);

            var byts = File.ReadAllBytes(arch);
            File.Delete(arch);
            return byts;
        }

        public void SetHtmlArchive(byte[] data, string projectPath)
        {
            string path = Path.Combine(projectPath, "Sources/Html.zip");
            File.WriteAllBytes(path, data);
        }

        public void SetScriptArchive(byte[] data, string projectPath)
        {
            string path = Path.Combine(projectPath, "Sources/Scripts.zip");
            File.WriteAllBytes(path, data);
        }

        protected virtual void ArchiveViews(MoldsterModule mod, string projectPath)
        {

            var vws = Path.Combine(Paths.ConfigRoot, "Views", mod.InstallPath);
            WriteFileOperation("Updating from", vws);
            if (Directory.Exists(vws))
            {
                var vwBytes = CompressFolder(vws, mod.AssemblyName + ".views");
                SetHtmlArchive(vwBytes, projectPath);
                WriteSuccess();
            }
            else
            {
                GotoColumn(SuccessCol);
                Out.Write("Not Found");
            }
            Out.WriteLine();
        }

        protected virtual void ArchiveScripts(MoldsterModule mod, string projectPath)
        {
            Out.WriteLine($"Updating module {mod.Name}");
            Utils.ClearDirectory(tmpLocation);

            var scripts = Path.Combine(Names.BaseFolder, mod.InstallPath);
            WriteFileOperation("Updating from", scripts);
            if (Directory.Exists(scripts))
            {
                var scriptsByts = CompressFolder(scripts, mod.AssemblyName + ".scripts");
                SetScriptArchive(scriptsByts, projectPath);
                WriteSuccess();
            }
            else
            {
                GotoColumn(SuccessCol);
                Out.Write("Not Found");
            }
            Out.WriteLine();
        }

        public virtual Result UpdateModuleFiles(string assemblyName)
        {
            if (MoldsterModulesConfig.GetProjectPath(assemblyName, out string projectPath))
            {
                try
                {

                    var mod = MoldsterModulesConfig.Modules.Where(d => d.AssemblyName == assemblyName).FirstOrDefault();
                    if (mod == null)
                        return new Result { Code = 1, Message = $"Module not registered '{assemblyName}'" };

                    ArchiveScripts(mod, projectPath);
                    ArchiveViews(mod, projectPath);
                    UpdateDefinitionJson(mod, projectPath);
                    return new Result(0);
                }
                catch (Exception ex)
                {
                    var res = new Result(1);
                    res.Message = "Failed";
                    res.SetException(ex);
                    return res;
                }
            }
            return new Result { Code = 1, Message = $"No project path registered for '{assemblyName}'" };
        }

        protected virtual void UpdateDefinitionJson(MoldsterModule mod, string projectPath)
        {
            mod.Categories = unit.PageCategoryRepository.GetByMoldsterModule(mod.InstallPath);
            mod.Resources = unit.ResourceRepository.GetByMoldsterModule(mod.InstallPath);

            WriteFileOperation("Updating", "Module.json");
            string path = Path.Combine(projectPath, "Module.json");
            File.WriteAllText(path, mod.ToJsonIndent());
            MoldsterModulesConfig.SetModule(mod);
            WriteSuccess();
            Out.WriteLine();
        }



        public IEnumerable<ModuleDTO> GetRegisteredModules()
        {
            return MoldsterModulesConfig.Modules.Select(d => new ModuleDTO { Name = d.Name, AssemblyName = d.AssemblyName }).ToList();
        }
    }
}
