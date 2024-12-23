using CodeShellCore.Data;

namespace CodeShellCore.Moldster.Navigation
{
    public class NavigationGroupDto : EntityDto<long>
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Chain { get; set; }
    }
}
