using CodeShellCore.Linq;
using CodeShellCore.Moldster.Navigation.Dtos;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Resources.Dtos;
//using Configurator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public static class SearchExpressions
    {
        public static void RegisterExpressions()
        {
            ExpressionStore.RegisterSearchExpression<PageListDTO>(term =>
            e => e.ViewPath.Contains(term) || e.PageCategoryName.Contains(term) || e.BaseComponent.Contains(term));

            ExpressionStore.RegisterSearchExpression<PageCategoryListDTO>(term => e => e.Name.Contains(term) || e.BaseComponent.Contains(term) || e.ResourceName.Contains(term));

            ExpressionStore.RegisterSearchExpression<NavigationGroupDTO>(term => e => e.Name.Contains(term));

            ExpressionStore.RegisterSearchExpression<NavigationPageDTO>(term => e => e.Url.Contains(term));

            ExpressionStore.RegisterSearchExpression<NavigationPageListDTO>(term => e => e.Url.Contains(term) ||
             e.Name.Contains(term));

            ExpressionStore.RegisterSearchExpression<PageControlListDTO>(term => e =>
            e.PageName.Contains(term) || e.ControlIdentifier.Contains(term) || e.ControlType.Contains(term));

            ExpressionStore.RegisterSearchExpression<ResourceListDTO>(term => e => e.Name.Contains(term) || e.Domain.Contains(term));

            ExpressionStore.RegisterSearchExpression<PageReferenceDTO>(term =>
            e => e.PageViewPath.Contains(term) || e.ParameterName.Contains(term) || e.ReferencedPageViewPath.Contains(term) || e.PageCategoryViewPath.Contains(term));
        }
    }
}
