using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Moldster.Razor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using CodeShellCore.Types;
using System.Linq;
using CodeShellCore.Text;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public static class MoldsterGeneralHelpers
    {
        /// <summary>
        /// <p>writes the opening tag name and the closing tag name on the end bracket</p>
        /// <p>the tag should be closed using <see cref="CloseContainer(IHtmlHelper, string)"/></p>
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="tagName">html tag name</param>
        /// <param name="identifier"></param>
        /// <param name="container">an instance of <see cref="FMSHtmlContainer"/></param>
        /// <param name="attributes"></param>
        /// <returns>identifier accessibility <see cref="Accessibility.Read"/></returns>
        public static bool MakeContainer<T>(this IHtmlHelper<T> helper, string tagName, string identifier, out MoldsterHtmlContainer container, object attributes = null)
        {

            bool access = helper.GetAccessibility(identifier).Read;
            if (access)
                container = new MoldsterHtmlContainer(helper, tagName, identifier, attributes);
            else
                container = new MoldsterHtmlContainer(helper, identifier, true);

            helper.AddToViewControls(container.Control);

            return access;
        }

        public static bool StartContainer<T>(this IHtmlHelper<T> helper, string tag, string identifier, object attrs = null, string tabVariable = null)
        {
            bool access = helper.GetAccessibility(identifier).Read;

            if (access)
            {
                dynamic other = new ExpandoObject();
                if (attrs == null || !attrs.HasProperty("id"))
                    other.id = identifier;
                if (tabVariable != null)
                {
                    other.calc__show_if = $"{tabVariable}=='{identifier}'";
                    other.@class = "tab-page";
                }


                HtmlContainer.WriteContainerStart(helper.ViewContext, tag, attrs, other);
            }

            var con = new ControlDTO
            {
                Identifier = identifier?.ToLower(),
                ControlType = "Container"
            };
            helper.AddToViewControls(con);
            return access;
        }

        /// <summary>
        /// gets a control accessibility using identifier
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CodeShellCore.Moldster.Razor.Accessibility GetAccessibility(this IHtmlHelper helper, string id)
        {
            if (helper.CollectingData())
                return new CodeShellCore.Moldster.Razor.Accessibility(2);

            return helper.Config().GetAccessibility(id.ToLower());
        }

        internal static bool CollectingData(this IHtmlHelper helper)
        {
            bool CollectViewData = false;
            object val;
            if (helper.ViewData.TryGetValue("CollectViewData", out val))
                CollectViewData = (bool)val;
            return CollectViewData;
        }

        internal static TemplateDataCollector GetCollector<T>(this IHtmlHelper<T> helper)
        {
            object val;
            TemplateDataCollector PageControls;
            if (helper.ViewData.TryGetValue("PageControls", out val))
            {
                PageControls = (TemplateDataCollector)val;
                if (PageControls.EntityName == null)
                    PageControls.EntityName = typeof(T).RealModelType().FullName;
            }
            else
            {
                Type entityType = typeof(T);

                helper.ViewData["PageControls"] = new TemplateDataCollector
                {
                    EntityName = typeof(T).RealModelType().FullName,
                    Controls = new List<ControlDTO>()
                };
                PageControls = (TemplateDataCollector)helper.ViewData["PageControls"];
            }
            return PageControls;
        }

        public static void AddToViewControls<T>(this IHtmlHelper<T> helper, ControlDTO cont)
        {
            if (!helper.CollectingData())
                return;

            var coll = helper.GetCollector();
            if (!coll.Controls.Any(d => d.Identifier == cont.Identifier))
                helper.GetCollector().Controls.Add(cont);
        }

        public static void AddSource<T>(this IHtmlHelper<T> helper, Lister lst, string identifier = null)
        {
            if (!helper.CollectingData())
                helper.Config().Sources.Add(lst);

            if (identifier != null)
            {
                var ctrl = new ControlDTO
                {
                    ControlType = "DataSource",
                    Identifier = identifier?.ToLower()
                };

                ctrl.Collection = new CollectionDTO
                {
                    Name = lst.CollecionName,
                };

                if (helper.Config().Controls.TryGetValue(identifier.ToLower(), out ControlDTO dto))
                {
                    if (dto.Collection != null)
                    {
                        lst.CollectionIdentifier = dto.Collection.Name;
                    }

                }

                helper.AddToViewControls(ctrl);
            }

        }


        public static IHtmlContent ComponentSelectorFromOther(this IHtmlHelper helper, string id, string def = null, object attr = null)
        {
            string res = "";

            string componentPath = helper.GetViewParams().GetFromOther(id, def);

            id = id != "none" ? "#" + id : null;

            if (!string.IsNullOrEmpty(componentPath))
            {
                componentPath = componentPath.GetAfterLast("/").LCFirst();
                string attrString = attr == null ? "" : RazorUtils.ToAttributeString(attr);
                res = $"<{componentPath} {id} [IsEmbedded]='true'{attrString}></{componentPath}>";
            }
            return new HtmlString(res);
        }
    }
}
