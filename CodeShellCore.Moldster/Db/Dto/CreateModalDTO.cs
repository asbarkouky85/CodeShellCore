using CodeShellCore.Moldster.Razor;

namespace CodeShellCore.Moldster.Dto
{
    public class CreateModalDTO
    {
        public long Id { get; set; }
        public string Domain { get; set; }
        public string TenantCode { get; set; }
        public string Name { get; set; }
        public string CategoryPath { get; set; }
        public long? CategoryId { get; set; }
        public ViewParams ViewParams { get; set; }
        public string Layout { get; set; }
    }
}
