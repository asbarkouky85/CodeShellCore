using CodeShellCore.Moldster.Db;
using CodeShellCore.Data.Recursion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class DomainRecursive
    {
        public long Id;
        public string Name;
        public List<DomainRecursive> SubDomains = new List<DomainRecursive>();
        public string NameChain;

        public static DomainRecursive ToDomainRecursive(IRecursiveModel dom)
        {
            var rec = new DomainRecursive
            {
                Id = dom.Id,
                Name = dom.Name,
                NameChain=dom.NameChain,
                SubDomains = new List<DomainRecursive>()
            };
            foreach (var c in dom.Children)
            {
                rec.SubDomains.Add(ToDomainRecursive(c));
            }
            return rec;
        }
    }
}
