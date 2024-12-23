using CodeShellCore.Data;

namespace CodeShellCore.Moldster.Navigation
{
    public class NavigationPageDto : EditableEntityDto<long>
    {
        public long? PageId { get; set; }
        public int DisplayOrder { get; set; }
        public long NavigationGroupId { get; set; }
    }
}
