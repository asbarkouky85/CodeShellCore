using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Dashboard
{
    public enum ChartTypes { Doughnut, Bar, NumberBox, Grid, Gauge }
    public static class DashboardExtensions
    {
        static IdentifierProcessor proc = new IdentifierProcessor();

        static string GetTypeSelector(ChartTypes t)
        {
            switch (t)
            {
                case ChartTypes.Doughnut:
                    return "doughnut-chart";
                case ChartTypes.Bar:
                    return "bar-chart";
                case ChartTypes.NumberBox:
                    return "number-box";
                case ChartTypes.Grid:
                    return "dashboard-grid";
                case ChartTypes.Gauge:
                    return "dashboard-gauge";
                default:
                    return null;
            }
        }

        public static IHtmlContent ChartBlock(
            this IHtmlHelper helper,
            ChartTypes type,
            string item,
            int size = 4,
            string loadedField="true",
            object attrs = null,
            object componentAttrs = null,
            string classes = null)
        {
            var acc = proc.Process(helper, "chart_block_" + item, "DashboardItem__" + type.ToString());
            if (!acc.Read)
            {
                return null;
            }
            else
            {

                var model = new DashboardBlockModel
                {
                    Attributes = attrs != null ? RazorUtils.ToAttributeString(attrs) : null,
                    ComponentAttrs = componentAttrs != null ? RazorUtils.ToAttributeString(componentAttrs) : null,
                    Selector = GetTypeSelector(type),
                    Size = size,
                    SingleItem = true,
                    DataField=item,
                    IsLoadedField=loadedField
                };
                return helper.GetComponent("Components/DashboardBlock", model);
            }

        }

        public static IHtmlContent DashboardItem(
            this IHtmlHelper helper,
            ChartTypes type,
            string id,
            string listName = null,
            string[] lists = null,
            bool combine = false,
            int size = 4,
            string isLoadedField = null,
            object attrs = null,
            object componentAttrs = null,
            string classes = null
            )
        {
            var acc = proc.Process(helper, id, "DashboardItem__" + type.ToString());
            if (!acc.Read)
            {
                return null;
            }
            else
            {

                var model = new DashboardBlockModel
                {
                    Attributes = attrs != null ? RazorUtils.ToAttributeString(attrs) : null,
                    ComponentAttrs = componentAttrs != null ? RazorUtils.ToAttributeString(componentAttrs) : null,
                    Selector = GetTypeSelector(type),
                    Size = size,
                    IsLoadedField = isLoadedField
                };

                List<string> listsArray = new List<string>();

                if (listName != null)
                {
                    model.DataField = $"GetPagedList('{listName}').list";
                    if (combine)
                    {
                        model.DataField = null;
                        model.Series = $"GetPagedList('{listName}').list";
                    }
                    if (model.IsLoadedField == null)
                        model.IsLoadedField = $"GetIsLoaded('{listName}')";
                    helper.AddSource(Lister.Make(listName), id);
                }
                else if (lists != null)
                {
                    List<string> fieldArray = new List<string>();
                    foreach (var l in lists)
                    {
                        fieldArray.Add(l);
                        listsArray.Add("GetFirst('" + l + "')");
                        helper.AddSource(Lister.Make(l), id);
                    }
                    model.Series = "[" + string.Join(",", listsArray) + "]";
                    if (model.IsLoadedField == null)
                        model.IsLoadedField = "GetAllIsLoaded(['" + string.Join("','", fieldArray) + "'])";
                }

                return helper.GetComponent("Components/DashboardBlock", model);
            }
        }
    }
}
