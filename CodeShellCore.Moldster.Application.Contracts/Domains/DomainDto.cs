using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainDto : EntityDto<long>
    {
        public long? ParentId { get; set; }
        public string DomainName { get; set; }
        public IEnumerable<DomainDto> SubDomains { get; set; }
        
        public void AppendChildren(IEnumerable<DomainDto> lst)
        {
            SubDomains = lst.Where(d => d.ParentId == Id);
            foreach (var s in SubDomains)
            {
                s.AppendChildren(lst);
            }
        }
    }
}
