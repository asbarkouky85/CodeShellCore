using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class NgModule
    {
        public string Name { get; set; }
        public IEnumerable<DomainWithPages> Domains { get; set; }
        public string BaseAppModulePath { get; set; }
        public bool UseLocalization { get; set; }
        public string MainComponent { get; set; }
        public string MainComponentBase { get; set; }
        public string Default { get; set; }
        public string BaseFolder { get; set; }

        public void SetDomainBaseFolder()
        {
            foreach (var d in Domains)
                d.BaseFolder = BaseFolder;
        }

        public DomainWithPages GetDomain(string st)
        {
            var dom = Domains.SingleOrDefault(d => d.Name == st);
            if (dom == null)
                throw new ArgumentOutOfRangeException($"No such domain {st} in module {Name}");
            return dom;
        }
    }
}
