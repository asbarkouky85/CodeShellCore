using CodeShellCore.Data;
using System.ComponentModel.DataAnnotations;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainDto : EntityDto<long>
    {
        [StringLength(50)]
        public string Name { get; set; }
        public long? ParentId { get; set; }

        public string Chain { get; set; }
        public string NameChain { get; set; }

    }
}
