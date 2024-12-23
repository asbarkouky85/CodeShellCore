using CodeShellCore.Data;

namespace CodeShellCore.Moldster.Pages
{
    public class PageControlDto : EntityDto<long>
    {
        public long ControlId { get; set; }
        public long PageId { get; set; }
        public byte Accessability { get; set; }
        public long? SourceCollectionId { get; set; }
        public bool? Persistent { get; set; }

    }
}
