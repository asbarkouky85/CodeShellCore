using CodeShellCore.Text;

namespace CodeShellCore.Web.Moldster
{
    public class IndexModel
    {
        public string Title { get; set; }
        public string Locale { get; set; }
        public string FooterMessage { get; set; }
        public IServerConfig Config { get; set; }
        public string ConfigString => Config.GetJson();

        public string PackageDomain { get; set; }
        public string PackageId { get; set; }
    }
}
