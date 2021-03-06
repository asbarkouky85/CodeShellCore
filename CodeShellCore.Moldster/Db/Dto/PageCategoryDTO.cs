﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class PageCategoryDTO
    {
        public PageCategory Category { get; set; }
        public string Resource { get; set; }
        public string ResourceDomain { get; set; }
        public string Domain { get; set; }
        public static Expression<Func<PageCategory, PageCategoryDTO>> Expression
        {
            get
            {
                return e => new PageCategoryDTO
                {
                    Category = e,
                    Resource = e.Resource == null ? null : e.Resource.Name,
                    Domain = e.Resource == null ? null : e.Domain.Name,
                    ResourceDomain = e.Resource == null ? null : (e.Resource.Domain == null ? null : e.Resource.Domain.NameChain)
                };
            }
        }

    }
}
