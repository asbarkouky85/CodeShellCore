using System.Collections.Generic;
using CodeShellCore.Tasks;
using CodeShellCore.Text;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Definitions;

namespace CodeShellCore.Moldster.Db.Services
{
    public class DbViewsHttpService : DefaultViewsService,IDbViewsService
    {
        public DbViewsHttpService(PathProvider paths):base(paths)
        {
        }
        
        public TemplateDataCollector GetTemplateData(long id)
        {
            string data = Get("GetTemplateData/" + id).Content.ReadAsStringAsync().GetTaskResult();
            return data.FromJson<TemplateDataCollector>() ?? new TemplateDataCollector { Controls = new List<ControlDTO>() };
        }

    }
}
