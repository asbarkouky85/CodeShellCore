using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services.Http;
using CodeShellCore.Tasks;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class DefaultViewsService : HttpService, IViewsService
    {
        readonly PathProvider Paths;
        protected override string BaseUrl { get { return Utils.CombineUrl(Paths.ConfigUrl, "Views"); } }

        public DefaultViewsService(PathProvider paths)
        {
            Paths = paths;
        }

        public string GetPage(PageAcquisitorDTO pageAcquisitorDTO)
        {
            var s = Get("GetPage", pageAcquisitorDTO);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public string GetPage(string viewPath)
        {
            var s = Get("GetPage/?ViewPath=" + viewPath);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public string GetMainComponent(string baseComponent)
        {
            var s = Get("GetMainComponent/?ViewPath=" + baseComponent);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public string GetGuide(string moduleCode)
        {
            var s = Get("GetGuide/" + moduleCode);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }
    }
}
