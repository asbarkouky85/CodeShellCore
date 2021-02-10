using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Files;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Text;
using System;
using System.IO;
using System.Linq;

namespace CodeShellCore.Moldster.Configurator.Services
{
    public class PageCategoryService : EntityService<PageCategory>
    {
        readonly IConfigUnit Unit;
        private readonly IFileHandler fileHandler;

        public PageCategoryService(IConfigUnit unit, IFileHandler fileHandler) : base(unit)
        {
            Unit = unit;
            this.fileHandler = fileHandler;

        }

        public override PageCategory GetSingle(object id)
        {
            var cat= base.GetSingle(id);
            if(cat!=null)
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

            var d = Unit.DomainRepository.GetOrCreatePath(obj.ViewPath.GetBeforeLast("/"));

            string template = Path.Combine(Shell.AppRootPath, "Views", obj.ViewPath + ".cshtml");
            if (!fileHandler.Exists(template))
                throw new Exception("No such template : " + template);

            d.PageCategories.Add(obj);
            obj.DomainId = d.Id;
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



    }
}
