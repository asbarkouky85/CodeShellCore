using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Recursion
{
    public interface IRecursiveModel : IEntity<long>
    {
        string Name { get; set; }
        long? ParentId { get; set; }
        string Chain { get; set; }
        string NameChain { get; set; }
        //bool HasContents { get; set; }
        //int ContentCount { get; set; }
        IEnumerable<IRecursiveModel> Children { get; set; }
    }
}
