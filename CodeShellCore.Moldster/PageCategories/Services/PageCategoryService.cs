using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Files;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeShellCore.Moldster.PageCategories.Services
{
    public class PageCategoryService : EntityService<PageCategory>
    {
        readonly IConfigUnit Unit;
        private readonly IFileHandler fileHandler;
        private readonly IPathsService conf;

        public PageCategoryService(IConfigUnit unit, IFileHandler fileHandler, IPathsService conf) : base(unit)
        {
            Unit = unit;
            this.fileHandler = fileHandler;
            this.conf = conf;
        }

        public override PageCategory GetSingle(object id)
        {
            var cat = base.GetSingle(id);
            if (cat != null)
            {
                cat.PageCategoryParameters = Unit.PageCategoryParameterRepository.Find(d => d.PageCategoryId.Equals(id));
                cat.Controls = Unit.ControlRepository.Find(d => d.PageCategoryId.Equals(id));
            }
            return cat;
        }

        public override SubmitResult Create(PageCategory obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
                obj.Name = obj.ViewPath?.GetAfterLast("/");

            var domain = Unit.DomainRepository.GetOrCreatePath(obj.ViewPath.GetBeforeLast("/"));

            string template = Path.Combine(Shell.AppRootPath, "Views", obj.ViewPath + ".cshtml");
            if (!fileHandler.Exists(template))
                throw new Exception("No such template : " + template);

            domain.PageCategories.Add(obj);
            obj.DomainId = domain.Id;
            if (obj.ResourceName != null)
            {
                string[] sp = obj.ResourceName.Split('/');
                string res = obj.ResourceName;
                string service = null;

                if (sp.Length > 1)
                {
                    res = sp[1];
                    service = sp[0];
                }

                Resource r = Unit.ResourceRepository.GetResource(res, service);

                string[] bases = new[] { "Edit", "List", "Tree" };
                if (bases.Contains(obj.BaseComponent) && r == null)
                {
                    throw new Exception("This " + obj.BaseComponent + " base component requires a Resource");
                }
                r.PageCategories.Add(obj);

                return Unit.SaveChanges();
            }
            return base.Create(obj);
        }

        public LoadResult<PageCategoryListDTO> GetAll(LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<PageCategoryListDTO>();
            return Unit.PageCategoryRepository.FindAs(PageCategoryListDTO.Expression, opts);
        }

        public LoadResult<PageCategoryListDTO> GetPagesCategoryByDomain(long domainId, LoadOptions opt)
        {
            return Unit.PageCategoryRepository.GetUnderDomain(domainId, opt);
        }

        public List<TemplateDTO> GetTemplates()
        {
            string configPath = conf.ConfigRoot;
            var DbTemplateList = Unit.PageCategoryRepository.GetValues(d => d.ViewPath);
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
                        CreatedOn = File.GetCreationTime(temp),
                        ResourceId = null,
                        BaseComponent = null
                    });
                }

            };

            return templateList.OrderByDescending(d => d.CreatedOn).ToList();
        }

        public SubmitResult Create(List<PageCategory> list)
        {
            List<Domain> doms = new List<Domain>();
            foreach (var item in list)
            {

                var d = Unit.DomainRepository.GetOrCreatePath(item.ViewPath.GetBeforeLast("/"), ref doms);
                Unit.PageCategoryRepository.Add(item, d);
            }
            return Unit.SaveChanges();
        }

    }
}
