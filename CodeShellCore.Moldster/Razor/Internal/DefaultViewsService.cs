using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Http;
using CodeShellCore.Tasks;
using System.Net;
using System;
using CodeShellCore.Text;
using CodeShellCore.Moldster.Dto;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Razor.Internal
{
    public class DefaultViewsService : HttpService, IViewsService
    {
        readonly IPathsService Paths;
        protected override string BaseUrl { get { return Utils.CombineUrl(Paths.ConfigUrl, "Views"); } }

        public DefaultViewsService(IPathsService paths)
        {
            Paths = paths;
        }

        public TemplateDataCollector GetTemplateData(long id)
        {
            string data = Get("GetTemplateData/" + id).Content.ReadAsStringAsync().GetTaskResult();
            return data.FromJson<TemplateDataCollector>() ?? new TemplateDataCollector { Controls = new List<ControlDTO>() };
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

        public string GetPageById(long id)
        {
            var s = Get("GetPageById/" + id);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public virtual bool CheckServer(out HttpResult res)
        {

            try
            {
                var x = new DefaultHttpService(Paths.ConfigUrl);
                var result = x.Get("");
                res = new HttpResult(result.StatusCode);
                return result.StatusCode == HttpStatusCode.OK;
            }
            catch (CodeShellHttpException ex)
            {
                res = new HttpResult(ex.Status, ex.Message);
                res.SetException(ex);
                return false;
            }
            catch (Exception exx)
            {
                res = new HttpResult(HttpStatusCode.NotFound, exx.Message);
                res.SetException(exx);
                return false;
            }
        }

    }
}
