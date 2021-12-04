using CodeShellCore.Text;

namespace CodeShellCore.Web.Moldster
{
    public class IndexModel
    {
        public string Title { get; set; }
        public string Locale { get; set; }
        public IServerConfig Config { get; set; }
    }
}
