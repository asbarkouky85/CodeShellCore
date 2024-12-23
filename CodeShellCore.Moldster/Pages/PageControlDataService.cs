using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageControlDataService : ServiceBase, IPageControlDataService
    {
        private readonly IMoldsterUnit Unit;

        public PageControlDataService(IMoldsterUnit unit)
        {
            Unit = unit;
        }

        public async Task<IEnumerable<DomainWithPagesDTO>> GetDomainWithPages(long tenantId, string domainName = null)
        {
            if (domainName != null)
                return await Unit.DomainRepository.FindAndMap<DomainWithPagesDTO>(n => n.Pages.Any(p => p.TenantId == tenantId) && n.Name == domainName);
            return await Unit.DomainRepository.FindAndMap<DomainWithPagesDTO>(n => n.Pages.Any(p => p.TenantId == tenantId));
        }

        public async Task<SubmitResult> UpdateTemplatePages(long template, long? tenant = null)
        {
            IEnumerable<Page> pages = await Unit.PageRepository.Find(d => d.PageCategoryId == template && (d.TenantId == tenant || tenant == null));
            var controls = await Unit.ControlRepository.Find(d => d.PageCategoryId == template);
            foreach (Page p in pages)
            {
                Unit.PageControlRepository.UpdateControls(p.Id, controls, (byte)p.DefaultAccessibility);
            }
            var s = await Unit.SaveChanges();
            return s;
        }

        public async Task<SubmitResult> UpdateTemplateControls(PageCategory cat, List<ControlRenderDto> cont)
        {
            IEnumerable<Control> current = await Unit.ControlRepository.Find(d => d.PageCategoryId == cat.Id);

            foreach (ControlRenderDto c in cont)
            {
                Control con = await GetControl(c, cat, current);
                if (!current.Contains(con))
                    Unit.ControlRepository.Add(con);
                con.ControlType = c.ControlType;
            }
            return await Unit.SaveChanges();
        }

        public async Task<SubmitResult> DeleteUnusedControls(PageCategory category, List<ControlRenderDto> controls)
        {
            List<string> sts = new List<string>();
            foreach (var c in controls)
                sts.AddRange(GetIdentifiers(c));

            var cons = await Unit.ControlRepository.Find(d => d.PageCategoryId == category.Id && !sts.Contains(d.Identifier));
            foreach (var c in cons)
                Unit.ControlRepository.Delete(c);
            return await Unit.SaveChanges(throwException: true);
        }


        public async Task<Control> GetControl(ControlRenderDto src, PageCategory cat, IEnumerable<Control> current)
        {
            Control con = current.Where(d => d.Identifier == src.Identifier).FirstOrDefault();

            if (con == null)
            {
                con = new Control
                {
                    Id = Utils.GenerateID(),
                    Identifier = src.Identifier,
                    ControlType = src.ControlType,
                    PageCategoryId = cat.Id
                };
            }

            foreach (ControlRenderDto cc in src.Children)
            {
                Control d = await GetControl(cc, cat, current);
                if (!current.Contains(d))
                    con.InverseParentControlNavigation.Add(d);
                d.ControlType = cc.ControlType;
            }

            return con;
        }



        private List<string> GetIdentifiers(ControlRenderDto dto)
        {
            List<string> st = new List<string>();
            st.Add(dto.Identifier);
            if (dto.Children != null)
            {
                foreach (var c in dto.Children)
                    st.AddRange(GetIdentifiers(c));
            }
            return st;
        }
    }
}
