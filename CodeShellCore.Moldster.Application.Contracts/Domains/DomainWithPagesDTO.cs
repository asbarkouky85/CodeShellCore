using CodeShellCore.Moldster.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainWithPagesDTO
    {

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string DomainName { get; set; }
        public IEnumerable<PageDetailsDto> Pages { get; set; }
        public IEnumerable<DomainWithPagesDTO> SubDomains { get; set; }

        public void AppendChildren(IEnumerable<DomainWithPagesDTO> lst)
        {
            SubDomains = lst.Where(d => d.ParentId == Id);
            foreach (var s in SubDomains)
            {
                s.AppendChildren(lst);
            }
        }
    }
}
