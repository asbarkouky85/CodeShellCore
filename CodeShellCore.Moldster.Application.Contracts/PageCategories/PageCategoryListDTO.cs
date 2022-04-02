namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryListDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string BaseComponent { get; set; }
        public string ResourceName { get; set; }
        public string DomainName { get; set; }
        public string Layout { get; set; }
        public string ViewPath { get; set; }
        public long? ResourceId { get; set; }

        public long? DomainId { get; set; }


    }
}
