using CodeShellCore.Cli;
using CodeShellCore.Data;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Services;
using CodeShellCore.Text;
using CodeShellCore.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public class ScriptModelMappingService : ConsoleService, IScriptModelMappingService
    {
        private readonly WriterService _writer;
        private readonly IMoldProvider _molds;
        private readonly IPathsService _paths;
        private readonly IConfigUnit unit;

        protected const int resultcol = 8;

        public ScriptModelMappingService(
            IMoldProvider mold,
            IPathsService paths,
            IConfigUnit unit,
            IOutputWriter output) : base(output)
        {
            _writer = new WriterService();
            _molds = mold;
            _paths = paths;
            this.unit = unit;

        }

        public string MapEnum<T>()
        {

            WriteFileOperation("Generating Enum", typeof(T).Name);
            var s = Enum.GetValues(typeof(T));
            var ty = Enum.GetUnderlyingType(typeof(T));
            var ret = "export enum " + typeof(T).Name + " {\n";
            foreach (var v in s)
            {
                ret += "\t" + v + " = " + Convert.ChangeType(v, ty) + " ,\n";
            }
            ret += "};\n\n";
            return ret;
        }

        public string MapEntity(Type t, bool listItem = false)
        {
            WriteFileOperation("Generating Model", t.Name);
            var lst = t.GetProperties().ToList();
            if (listItem)
            {
                lst = lst.Where(d => d.Name != "State").ToList();
            }
            string cl = "export class " + t.Name + (listItem ? " extends ListItem" : "") + " {";
            foreach (var prop in lst)
            {
                cl += "\n\t" +
                    prop.Name.LCFirst() +
                    (prop.PropertyType.IsNullable() ? "?: null |" : ":") + " " +
                    GetTsType(prop.PropertyType) +
                    GetInitializer(prop.PropertyType) + ";";
            }
            cl += "\n}\r\n\n";
            Out.WriteLine("Type : " + t.Name);
            return cl;
        }

        public string MapEntity<T>(bool listItem = false) where T : class
        {
            return MapEntity(typeof(T), listItem);
        }

        public string GetTsType(Type t)
        {
            if (t == typeof(string))
                return "string";

            if (t.RealType() == typeof(bool))
                return "boolean";

            if (t.RealType() == typeof(DateTime))
                return "Date";

            if (t.RealType().IsDecimalType() || t.RealType().IsIntgerType())
                return "number";

            if (typeof(IEnumerable).IsAssignableFrom(t))
                return "any[]";


            return "any";
        }

        public string GetInitializer(Type t)
        {
            if (t.IsNullable())
                return "";
            if (t == typeof(string))
                return " = \"\" ";

            if (t.RealType() == typeof(bool))
                return " = false";

            if (t.RealType().IsDecimalType() || t.RealType().IsIntgerType())
                return " = 0";

            if (t.RealType() == typeof(DateTime))
                return " = new Date() ";

            if (typeof(IEnumerable).IsAssignableFrom(t))
                return " = []";

            return " = {}";
        }

        //public void GenerateEnums()
        //{
        //    if (_enums.IsActive)
        //    {
        //        var pat = Path.Combine(_paths.UIRoot, _enums.FilePath);
        //        File.WriteAllText(pat, _enums.FileWriter(this));
        //    }
        //}

        public void GenerateDtos(string assembly, bool listItem = false)
        {
            var dtos = Assembly.Load(assembly).GetTypes().Where(d => d.GetInterfaces().Contains(typeof(IDTO)));

            IEnumerable<string> domains = unit.DomainRepository.GetValues(d => d.Name, d => d.ParentId == null);

            Dictionary<string, List<string>> models = new Dictionary<string, List<string>>();

            foreach (var d in domains)
            {
                models[d] = new List<string>();
            }

            foreach (Type t in dtos)
            {
                string domain = t.FullName.GetBeforeLast(".");
                domain = domain.GetAfterLast(".");
                if (domain.ToLower().StartsWith("dto"))
                    domain = t.FullName.GetBeforeLast(".").GetBeforeLast(".").GetAfterLast(".");
                if (models.ContainsKey(domain))
                {
                    string modelScript = MapEntity(t, listItem);
                    models[domain].Add(modelScript);
                }
            }

            foreach (var item in models)
            {
                string path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, item.Key, "Dtos.ts");
                string script = listItem ? "import { ListItem } from 'codeshell/helpers';\r\n" : "";
                if (item.Value.Any())
                {
                    script += string.Join("\r\n\r\n", item.Value);
                }
                Utils.CreateFolderForFile(path);
                File.WriteAllText(path, script);
            }
        }

        public void ScriptMapping()
        {
            foreach (var map in ScriptMapSettings.Mappings)
            {
                string f = map.FilePath;
                if (!f.ToLower().EndsWith(".ts"))
                    f += ".ts";
                var pat = Path.Combine(_paths.UIRoot, f);

                WriteColored("Writing file " + f + " : ", ConsoleColor.Cyan);
                Out.WriteLine();
                var s = map.FileWriter(this);
                s = "import { ListItem } from 'codeshell/helpers';\r\n" + s;
                File.WriteAllText(pat, s);
            }
        }
    }
}
