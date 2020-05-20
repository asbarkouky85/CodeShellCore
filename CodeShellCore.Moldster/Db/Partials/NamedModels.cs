using CodeShellCore.MQ;
using CodeShellCore.Data.Recursion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CodeShellCore.Data.Lookups;

namespace CodeShellCore.Moldster.Db
{
    public partial class Resource : INamed<long> { }
    public partial class DomainEntityCollection : INamed<long> { }
    public partial class Domain : IRecursiveModel
    {
        [NotMapped]
        public bool HasContents { get; set; }
        [NotMapped]
        public int ContentCount { get; set; }
        [NotMapped]
        public IEnumerable<IRecursiveModel> Children { get; set; }
    }
}
