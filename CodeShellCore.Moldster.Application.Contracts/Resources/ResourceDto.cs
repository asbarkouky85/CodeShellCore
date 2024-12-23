using CodeShellCore.Data;
using System.ComponentModel.DataAnnotations;

namespace CodeShellCore.Moldster.Resources
{
    public class ResourceDto : EntityDto<long>
    {
        public string Name { get; set; }
        [StringLength(50)]
        public string ServiceName { get; set; }
        public long? DomainId { get; set; }

    }
}
