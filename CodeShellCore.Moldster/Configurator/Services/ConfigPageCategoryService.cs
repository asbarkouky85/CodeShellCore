using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeShellCore.Moldster.Configurator.Services
{
    public class ConfigPageCategoryService : PageCategoryService
    {
        private readonly IConfigUnit _unit;
        private readonly IFileHandler _fileHandler;
        private readonly IPathsService conf;

        // private readonly PathProvider _paths;
        public ConfigPageCategoryService(IConfigUnit unit, IFileHandler fileHandler, IPathsService conf) : base(unit, fileHandler)
        {
            _unit = unit;
            _fileHandler = fileHandler;
            this.conf = conf;
        }

        public LoadResult<PageCategoryListDTO> GetAll(LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<PageCategoryListDTO>();
            return _unit.PageCategoryRepository.FindAs(PageCategoryListDTO.Expression, opts);
        }

        public LoadResult<PageCategoryListDTO> GetPagesCategoryByDomain(long domainId, LoadOptions opt)
        {
            return _unit.PageCategoryRepository.GetUnderDomain(domainId, opt);
        }

        public List<TemplateDTO> GetTemplates()
        {
            string configPath = conf.ConfigRoot;
            var DbTemplateList = _unit.PageCategoryRepository.GetValues(d => d.ViewPath);
            return GetLocalTemplate(DbTemplateList);
        }

        public List<TemplateDTO> GetLocalTemplate(IEnumerable<string> files)
        {
            var configPath = Path.Combine(conf.ConfigRoot, "Views");
            List<TemplateDTO> templateList = new List<TemplateDTO>();
            var templates = Directory.GetFiles(configPath, "*.cshtml", SearchOption.AllDirectories);

            foreach (var temp in templates)
            {
                var vPath = temp.Replace(configPath + "\\", "").Replace("\\", "/").Replace(".cshtml", "");
                var name = vPath.GetAfterLast("/");
                if (!files.Any(d => d == vPath) && name[0] != '_')
                {
                    templateList.Add(new TemplateDTO
                    {
                        Name = name,
                        ViewPath = vPath,
                        CreatedOn = File.GetCreationTime(temp)
                    });
                }

            };

            return templateList.OrderByDescending(d => d.CreatedOn).ToList();
        }

        public SubmitResult Create(List<PageCategory> list)
        {
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.Name))
                    item.Name = item.ViewPath?.GetAfterLast("/");

                var d = _unit.DomainRepository.GetOrCreatePath(item.ViewPath.GetBeforeLast("/"));
                item.Id = Utils.GenerateID();
                d.PageCategories.Add(item);
                item.DomainId = d.Id;
            }
            return _unit.SaveChanges();
        }

    }
}
