namespace CodeShellCore.Moldster.Sql
{
    public class SyncResult
    {
        public string SourceTenant { get; set; }
        public string TargetTenant { get; set; }
        public int AddedPages { get; set; }
        public int UpdatedPages { get; set; }
        public int AddedPageControls { get; set; }
        public int UpdatedPageControls { get; set; }
        public int NavigationPages { get; set; }
    }
}
