using System;
using System.Linq;
using System.Collections.Generic;

using CodeShellCore.Moldster.Db.Dto;

using CodeShellCore.Data.Helpers;
using CodeShellCore.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Db.Data;

namespace CodeShellCore.Moldster.Db.Services
{
    public class PageControlsService : EntityService<PageControl>
    {
        protected IConfigUnit Unit;

        public PageControlsService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }

        public IEnumerable<DomainWithPagesDTO> GetDomainWithPages(long tenantId, string domainName = null)
        {
            if (domainName != null)
                return Unit.DomainRepository.FindAs(DomainWithPagesDTO.RenderingExpression, n => n.Pages.Any(p=>p.TenantId == tenantId) && n.Name == domainName);
            return Unit.DomainRepository.FindAs(DomainWithPagesDTO.RenderingExpression, n => n.Pages.Any(p => p.TenantId == tenantId));
        }

        public PageOptions GetPageOptions(long pageId)
        {
            List<ControlDTO> lst = new List<ControlDTO>();

            PageOptions opts = Unit.PageRepository.FindSingleAs(d => new PageOptions
            {
                PageId = pageId,
                PageIdentifier = d.Domain.Name + "__" + d.Name,
                ViewParamsString = d.ViewParams,
                Layout = d.Layout + ".cshtml",
                ViewPath = d.PageCategory.ViewPath,
                DefaultAccessibility = d.DefaultAccessibility,
            }, e => e.Id == pageId);
            lst = Unit.PageControlRepository.GetDtos(e => e.PageId == pageId);

            opts.Controls = new Dictionary<string, ControlDTO>();
            var rep = new List<string>();
            foreach (var d in lst)
            {
                if (opts.Controls.ContainsKey(d.Identifier))
                    rep.Add(d.Identifier);

                opts.Controls[d.Identifier] = d;
            }
            opts.RepeatedIds = rep;

            return opts;
        }

        public Control GetControl(ControlDTO src, PageCategory cat, IEnumerable<Control> current, IEnumerable<DomainEntityProperty> props)
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

                if (src.Identifier.Contains("__"))
                {
                    string property = src.Identifier.GetAfterLast("__");
                    DomainEntityProperty prop = props.Where(d => d.Name == property).FirstOrDefault();

                    if (prop == null)
                    {
                        prop = new DomainEntityProperty
                        {
                            Id = Utils.GenerateID(),
                            Name = property,
                            DomainEntityId = cat.DomainEntityId,
                        };
                        Unit.EntityPropertyRepository.Add(prop);
                    }
                    prop.Controls.Add(con);
                }
            }

            foreach (ControlDTO cc in src.Children)
            {
                Control d = GetControl(cc, cat, current, props);
                if (!current.Contains(d))
                    con.InverseParentControlNavigation.Add(d);
                d.ControlType = cc.ControlType;
            }

            return con;
        }

        public SubmitResult UpdateTemplatePages(long template, long? tenant = null)
        {
            IEnumerable<Page> pages = Unit.PageRepository.Find(d => d.PageCategoryId == template && (d.TenantId == tenant || tenant == null));
            var controls = Unit.ControlRepository.Find(d => d.PageCategoryId == template);
            foreach (Page p in pages)
            {
                Unit.PageControlRepository.UpdateControls(p.Id, controls, (byte)p.DefaultAccessibility);
            }
            return Unit.SaveChanges();
        }

        public SubmitResult UpdateTemplateControls(PageCategory cat, IEnumerable<ControlDTO> cont)
        {
            IEnumerable<DomainEntityProperty> p = Unit.EntityPropertyRepository.Find(d => d.DomainEntityId == cat.DomainEntityId);
            IEnumerable<Control> current = Unit.ControlRepository.Find(d => d.PageCategoryId == cat.Id);

            foreach (ControlDTO c in cont)
            {
                Control con = GetControl(c, cat, current, p);
                if (!current.Contains(con))
                    Unit.ControlRepository.Add(con);
                con.ControlType = c.ControlType;
            }
            return Unit.SaveChanges();
        }

        private List<string> GetIdentifiers(ControlDTO dto)
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

        public SubmitResult DeleteUnusedControls(PageCategory category, List<ControlDTO> controls)
        {
            List<string> sts = new List<string>();
            foreach (var c in controls)
                sts.AddRange(GetIdentifiers(c));

            var cons = Unit.ControlRepository.Find(d => d.PageCategoryId == category.Id && !sts.Contains(d.Identifier));
            foreach (var c in cons)
                Unit.ControlRepository.Delete(c);
            return Unit.SaveChanges();
        }
    }
}
