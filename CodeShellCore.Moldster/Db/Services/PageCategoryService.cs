using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Text;
using System;
using System.IO;
using System.Linq;

namespace CodeShellCore.Moldster.Db.Services
{
    public class PageCategoryService : EntityService<PageCategory>
    {
        readonly IConfigUnit Unit;
        public PageCategoryService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }

        public override SubmitResult Create(PageCategory obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
                obj.Name = obj.ViewPath?.GetAfterLast("/");

            string template = Path.Combine(Shell.AppRootPath, "Views", obj.ViewPath + ".cshtml");
            if (!File.Exists(template))
                throw new Exception("No such template : " + template);
            if (obj.ResourceName != null)
            {
                string[] sp = obj.ResourceName.Split('/');
                if (sp.Length > 1)
                {
                    long domainId = Unit.DomainRepository.GetSingleValue(d => d.Id, d => d.Name == sp[0]);
                    if (domainId != 0)
                    {
                        Resource r = Unit.ResourceRepository.GetResource(domainId, sp[1]);
                        r.PageCategories.Add(obj);
                        return Unit.SaveChanges();
                    }
                }
                string[] bases = new[] { "Edit", "List", "Tree" };
                if (bases.Contains(obj.BaseComponent) && obj.Resource == null)
                {
                    throw new Exception("This " + obj.BaseComponent + " base component requires a Resource");
                }
            }
            return base.Create(obj);
        }
    }
}
