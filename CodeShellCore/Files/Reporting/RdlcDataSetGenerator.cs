using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Types;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeShellCore.Files.Reporting
{
    public class RdlcDataSetGenerator : ServiceBase
    {
        readonly WriterService Service;
        public RdlcDataSetGenerator(WriterService service)
        {
            Service = service;
        }

        public string Bind<T>(T model, bool save = true) where T : ReportModel
        {
            string path = Shell.ReportsRoot;
            var file = Path.Combine(path, model.Template+".rdlc");
            if (!File.Exists(file))
            {
                Utils.CreateFolderForFile(file);
                File.WriteAllText(file,RdlcXmlTemplates.ReportTemplate);
            }
            
            var contents = File.ReadAllText(file);
            var dataSets = GenerateDataSets<T>();
            var dataSources = GenerateDataSource<T>();

            Regex sourcesPattern = new Regex("<DataSources>((.|\n)*)</DataSources>");
            Regex setsPattern = new Regex("<DataSets>((.|\n)*)</DataSets>");

            contents = sourcesPattern.Replace(contents, "<DataSources>" + dataSources + "</DataSources>");
            contents = setsPattern.Replace(contents, "<DataSets>" + dataSets + "</DataSets>");

            File.WriteAllText(file, contents);
            return contents;
        }

        public string GenerateDataSets<T>() where T : ReportModel
        {

            var field = RdlcXmlTemplates.FieldTemplate;
            var dataSet = RdlcXmlTemplates.DataSetTemplate;

            var props = typeof(T).GetProperties();

            string ret = "";
            foreach (var p in props)
            {

                if (p.PropertyType.Implements(typeof(IEnumerable)) && p.PropertyType != typeof(string))
                {

                    var t = p.PropertyType.GetGenericArguments();
                    if (t.Length > 0)
                    {
                        DataSetModel ds = new DataSetModel
                        {
                            DataSourceName = typeof(T).Name,
                            Name = p.Name,
                            Fields = ""
                        };
                        var inProps = t[0].GetProperties();
                        foreach (var inP in inProps)
                        {
                            FieldModel f = new FieldModel
                            {
                                Name = inP.Name,
                                Type = inP.PropertyType.ToString()
                            };
                            ds.Fields += Service.FillStringParameters(field, f);
                        }
                        ret += Service.FillStringParameters(dataSet, ds);
                    }
                }
            }

            return ret;
        }

        public string GenerateDataSource<T>() where T : ReportModel
        {
            var source = RdlcXmlTemplates.DataSourceTemplate;
            DataSetModel ds = new DataSetModel
            {
                Name = typeof(T).Name
            };
            return Service.FillStringParameters(source, ds);
        }
    }
}
