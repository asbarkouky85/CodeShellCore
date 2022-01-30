using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class FolderContent
    {
        public long Id { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        public long FolderId { get; set; }
    }
}
