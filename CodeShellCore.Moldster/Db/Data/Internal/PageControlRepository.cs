using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Dto;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class PageControlRepository : MoldsterRepository<PageControl, ConfigurationContext>,IPageControlRepository
    {
        public PageControlRepository(ConfigurationContext con) : base(con)
        {
        }

        public List<ControlDTO> GetDtos(Expression<Func<PageControl, bool>> filter = null)
        {
            var q = Loader;
            if (filter != null)
                q = q.Where(filter);
            var dtoq = q.Select(d => new ControlDTO
            {
                Identifier = d.Control.Identifier,
                Accessibilty = d.Accessability,
                Collection = d.SourceCollection != null ? new CollectionDTO
                {
                    Id = d.SourceCollection.Id,
                    Name = d.SourceCollection.Name
                } : null,
                ParentId = d.Control.ParentControl,
                ControlType = d.Control.ControlType,
                CustomValidators = d.PageControlValidators.Select(v => v.Validator).ToList(),
                DefaultValidators = d.Control.ControlValidators.Select(v => v.Validator).ToList()
            });

            return dtoq.ToList();
        }

        public void UpdateControls(long pageId, List<Control> controls, byte defaultAccess = 2)
        {
            IEnumerable<PageControl> existing = Loader.Where(d => d.PageId == pageId).ToList();
            foreach (Control c in controls)
            {
                if (!existing.Any(d => d.ControlId == c.Id))
                {
                    Add(new PageControl
                    {
                        Id = Utils.GenerateID(),
                        PageId = pageId,
                        ControlId = c.Id,
                        Accessability = defaultAccess
                    });
                }
            }

        }
    }
}
