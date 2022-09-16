namespace CodeShellCore.Moldster.Pages.Views
{
    public class PageCategoryParameterWithPageId
    {
        public string ParameterName { get; set; }
        public string DefaultValue { get; set; }
        public long? LinkedPageId { get; set; }
        public int Type { get; set; }
        public long PageCategoryParameterId { get; set; }
    }
}
