namespace CodeShellCore.Moldster.Sql
{
    public class DbCreationRequest
    {
        public bool? Recursive { get; set; } = true;
        public bool? ReplaceExisting { get; set; }
        public bool? Force { get; set; }
        public string Environment { get; set; }
        public string TenantCode { get; set; }
        public string DbName { get; set; }
        public long? Id { get; set; }
    }
}
