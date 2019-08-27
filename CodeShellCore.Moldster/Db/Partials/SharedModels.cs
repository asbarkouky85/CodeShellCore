using CodeShellCore.MQ;
using CodeShellCore.Services.Recursive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeShellCore.Moldster.Db
{
    public partial class Resource : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return new string[] { "Domain" };
        }
    }
    public partial class Domain : ISharedModel, IRecursiveModel
    {
        [NotMapped]
        public bool HasContents { get; set; }
        [NotMapped]
        public int ContentCount { get; set; }
        [NotMapped]
        public IEnumerable<IRecursiveModel> Children { get; set; }

        public string[] GetNavPropertyNames()
        {
            return null;
        }
    }

    public partial class ResourceAction : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return new string[] { "Resource" };
        }
    }
}
