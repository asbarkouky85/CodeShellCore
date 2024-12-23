using System.Collections.Generic;

namespace CodeShellCore.Moldster.Environments
{
    public class MoldsterEnvironmentDto
    {
        public IEnumerable<string> Databases { get; set; }
        public string SourceDatabase { get; set; }
        public string Name { get; set; }
    }
}