namespace CodeShellCore.Data
{
    public class DatabaseFile
    {
        public string LogicalName { get; set; }
        public string PhysicalName { get; set; }
    }

    public class SqlPaths
    {
        public string DefaultFile { get; set; }
        public string DefaultLog { get; set; }
    }

    public class CountModel
    {
        public int Count { get; set; }
    }
}
