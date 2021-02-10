using CodeShellCore.Data.Recursion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class RecursionModel : IRecursiveModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IRecursiveModel> Children { get; set; }
        
        public long? ParentId { get; set; }
        public string Chain { get; set; }
        public string NameChain { get; set; }
        public bool HasContents { get; set; }
        public int ContentCount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
    }
}
