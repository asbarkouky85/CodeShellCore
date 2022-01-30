using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{
    public class PageCategoryParameterRepository : MoldsterRepository<PageCategoryParameter, MoldsterContext>, IPageCategoryParameterRepository
    {
        public PageCategoryParameterRepository(MoldsterContext con) : base(con)
        {
        }

        public IEnumerable<PageCategoryParameterWithPageId> FindForPageParameterUpdate(long id, long tenantId)
        {
            var text = (int)PageParameterTypes.Text;
            var empType = (int)PageParameterTypes.Embedded;
            var pq = from d in DbContext.Pages
                     where d.TenantId == tenantId
                     select d;
            var q = from d in Loader
                    where d.PageCategoryId==id
                    select new PageCategoryParameterWithPageId
                    {
                        DefaultValue = d.DefaultValue,
                        LinkedPageId = d.Type != text && d.DefaultValue != null ?
                        (long?)pq
                        .Where(e => (d.Type == empType && e.Name == d.DefaultValue) || (d.Type != empType && e.ViewPath == d.DefaultValue))
                        .Select(e => e.Id)
                        .FirstOrDefault() : null,
                        ParameterName = d.Name,
                        Type = d.Type,
                        PageCategoryParameterId = d.Id
                    };
            return q.ToList();
        }

        public void UpdateParameters(long id, List<PageCategoryParameterDTO> parameters)
        {
            var existing = Find(d => d.PageCategoryId == id);
            foreach (var p in parameters)
            {
                var ex = existing.FirstOrDefault(d => d.Name == p.Name);
                if (ex == null)
                {

                    ex = p.MapTo<PageCategoryParameter>();
                    ex.Id = Utils.GenerateID();
                    ex.PageCategoryId = id;
                    Add(ex);
                }
                else
                {
                    ex.AppendProperties(p);
                    Update(ex);
                }
            }
        }
    }
}
