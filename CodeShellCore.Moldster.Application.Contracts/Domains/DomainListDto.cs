using CodeShellCore.Data.Recursion;
using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{

    public class DomainListDto : EntityDto<long>, IRecursiveModel<DomainListDto>
    {
        public IEnumerable<DomainListDto> Children { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Chain { get; set; }
        public string NameChain { get; set; }
    }
}
