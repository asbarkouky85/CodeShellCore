namespace CodeShellCore.Moldster.Pages
{
    public class ParameterRequestDTO
    {
        public ParameterRequestTypes? Type { get; set; }
        public long? ReferencedByPageId { get; set; }
        public long? ReferencedPageId { get; set; }
        public string TenantCode { get; set; }
        public int? ParameterTypeId { get; set; }
    }
}
