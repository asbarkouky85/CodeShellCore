using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Files;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Text;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace CodeShellCore.Moldster.Db.Services
{
    public class PageCategoryService : EntityService<PageCategory>
    {
        readonly IConfigUnit Unit;
        private readonly IFileHandler fileHandler;
        private readonly DomainService domainService;

        public PageCategoryService(IConfigUnit unit, IFileHandler fileHandler, DomainService domainService) : base(unit)
        {
            Unit = unit;
            this.fileHandler = fileHandler;
            this.domainService = domainService;
        }

        public override SubmitResult Create(PageCategory obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
                obj.Name = obj.ViewPath?.GetAfterLast("/");

            var d = domainService.CreatePathAndGetId(obj.ViewPath.GetBeforeLast("/"));
            obj.DomainId = (long)d.Data["LastId"];

            string template = Path.Combine(Shell.AppRootPath, "Views", obj.ViewPath + ".cshtml");
            if (!fileHandler.Exists(template))
                throw new Exception("No such template : " + template);
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
                if (r == null)
                    return new SubmitResult((int)HttpStatusCode.BadRequest, "Resource "+res+" not found");
                r.PageCategories.Add(obj);

                string[] bases = new[] { "Edit", "List", "Tree" };
                if (bases.Contains(obj.BaseComponent) && r == null)
                {
                    throw new Exception("This " + obj.BaseComponent + " base component requires a Resource");
                }
            }
            return base.Create(obj);
        }
    }
}
