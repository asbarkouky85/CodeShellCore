namespace CodeShellCore.Security.Authorization
{
    public class ResourceV
    {
        public string Id { get; set; }
        public bool CanInsert { get; set; }
        public bool CanDelete { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanViewDetails { get; set; }

        
    }
}
