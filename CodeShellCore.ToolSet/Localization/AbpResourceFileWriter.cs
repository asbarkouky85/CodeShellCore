using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Localization
{
    public class AbpResourceFileWriter
    {
        public Dictionary<string, AbpResourceFile> Contents { get; set; }
        public string Folder { get; set; }
        public AbpResourceFileWriter(string root)
        {
            Folder = root;
            _read();
        }

        private void _read()
        {
            var files = Directory.GetFiles(Folder, "*.json");
            Contents = new Dictionary<string, AbpResourceFile>();
            foreach (var resPath in files)
            {
                var txt = File.ReadAllText(resPath);
                var name = new FileInfo(resPath).Name.Replace(".json", "");

                AbpResourceFile file = JsonConvert.DeserializeObject<AbpResourceFile>(txt);

                Contents.Add(name, file);
            }
        }

        public async Task AppendKeys(IEnumerable<string> keys)
        {
            foreach (var file in Contents)
            {
                file.Value.AppendKeys(keys);
                var writePath = Path.Combine(Folder, file.Key + ".json");
                await File.WriteAllTextAsync(writePath, JsonConvert.SerializeObject(file.Value, Formatting.Indented));
            }
        }
    }
}
