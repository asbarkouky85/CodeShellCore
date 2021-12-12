using CodeShellCore.Moldster;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Resources.Dtos;
using CodeShellCore.Types;
using CodeShellCore.Web.Razor.Containers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public static class MoldsterGeneralHelperExtensions
    {
        static IdentifierProcessor proc = new IdentifierProcessor();
        //static IMoldsterGeneralHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IMoldsterGeneralHelper>(); } }

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
        public static bool MakeContainer(this IHtmlHelper helper, string tagName, string identifier, out MoldsterHtmlContainer container, object attributes = null)
        {

            bool access = helper.GetAccessibility(identifier).Read;
            if (access)
                container = new MoldsterHtmlContainer(helper, tagName, identifier, attributes);
            else
                container = new MoldsterHtmlContainer(helper, identifier, true);

            helper.AddToViewControls(container.Control);

            return access;
        }



        public static bool ShowContent(this IHtmlHelper helper, string identifier)
        {
            var access = proc.Process(helper, identifier.ToLower(), "Container");
            return access.Read;
        }

        public static bool StartContainer(this IHtmlHelper helper, string tag, string identifier, object attrs = null, string tabVariable = null)
        {
            var access = proc.Process(helper, identifier.ToLower(), "Container");

            if (access.Read)
            {
                dynamic other = new ExpandoObject();
                string stringified = attrs == null ? "" : RazorUtils.ToAttributeString(attrs);
                if (attrs == null || !attrs.HasProperty("id"))
                    other.id = identifier;
                if (tabVariable != null)
                {
                    other.calc__show_if = $"{tabVariable}=='{identifier}'";
                    other.@class = "tab-page";
                }

                if (other != null)
                    stringified += RazorUtils.ToAttributeStringDynamic(other);
                stringified += " #" + identifier;
                helper.ViewContext.Writer.Write(string.Format("<{0} {1}>\n", tag, stringified));
            }

            return access.Read;
        }

        /// <summary>
        /// gets a control accessibility using identifier
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Accessibility GetAccessibility(this IHtmlHelper helper, string id)
        {
            if (helper.CollectingData())
                return new Accessibility(2);

            return helper.Config().GetAccessibility(id.ToLower());
        }

        public static Accessibility GetAccessibility<T, TVal>(this IHtmlHelper<T> helper, Expression<Func<T, TVal>> ex, bool cell = false)
        {
            if (helper.CollectingData())
                return new Accessibility(2);

            var id = RazorUtils.GetColumnId(ex);
            if (cell)
                id = "cell__" + id;
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

        internal static TemplateDataCollector GetCollector(this IHtmlHelper helper)
        {
            object val;
            TemplateDataCollector PageControls;
            if (helper.ViewData.TryGetValue("PageControls", out val))
            {
                PageControls = (TemplateDataCollector)val;
                if (PageControls.EntityName == null)
                {
                    Type t = helper.GetType().GenericTypeArguments.FirstOrDefault();
                    PageControls.EntityName = t == null ? "None" : t.RealModelType().FullName;
                }
            }
            else
            {
                Type t = helper.GetType().GenericTypeArguments.FirstOrDefault();
                helper.ViewData["PageControls"] = new TemplateDataCollector
                {
                    EntityName = t == null ? "None" : t.RealModelType().FullName,
                    Controls = new List<ControlDTO>()
                };
                PageControls = (TemplateDataCollector)helper.ViewData["PageControls"];
            }
            return PageControls;
        }

        public static void AddToViewControls(this IHtmlHelper helper, ControlDTO cont)
        {
            if (!helper.CollectingData())
                return;

            var coll = helper.GetCollector();
            if (!coll.Controls.Any(d => d.Identifier == cont.Identifier))
                helper.GetCollector().Controls.Add(cont);
        }

        public static string GetCollectionName(this IHtmlHelper helper, string identifier)
        {
            if (helper.Config().Controls.TryGetValue(identifier.ToLower(), out ControlDTO dto))
            {
                if (dto.Collection != null)
                {
                    return dto.Collection.Name;
                }
            }
            return null;
        }

        public static void AddSource(this IHtmlHelper helper, Lister lst, string identifier = null)
        {
            if (!helper.CollectingData() && lst.IsLookup)
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
            var Provider = helper.GetService<IMoldsterGeneralHelper>();
            return Provider.ComponentSelectorFromOther(helper, id, def, attr);
        }

        public static void AddModal(this IHtmlHelper helper, string id, string def = null)
        {
            var Provider = helper.GetService<IMoldsterGeneralHelper>();
            Provider.AddModal(helper, id, def);
        }

        /// <summary>
        /// Adds parameter to page category in the database
        /// </summary>
        /// <param name="id">the identifier of the view parameter</param>
        /// <param name="def">the default value of the database value is empty</param>
        public static void AddParameter(this IHtmlHelper helper, string id, string def = null)
        {
            var Provider = helper.GetService<IMoldsterGeneralHelper>();
            Provider.AddParameter(helper, id, def);
        }

        /// <summary>
        /// Returns <see cref="IHtmlContent"/> presenting a link to another component, processed as view parameter
        /// </summary>
        /// <param name="id">The identifier of the link view parameter that is obtained or stored in database json</param>
        /// <param name="def">The default value for that link filled if the view parameter is empty</param>
        /// <param name="idProperty">if not null added after the link and the model name (ex : '/[linkValue]/'+[ngModelName].[idProperty]</param>
        /// <param name="idExpression">if not null added after the link (ex: '/[linkValue]/'+[idExpression]</param>
        /// <returns></returns>
        public static IHtmlContent GetLinkRaw(this IHtmlHelper helper, string id, string def, string idProperty = null, string idExpression = null)
        {
            var lnk = new PageLink(id, def, idProperty, idExpression);
            return helper.Raw(helper.GetLink(lnk));
        }

        /// <summary>
        /// Returns a string presenting a link to another component, processed as view parameter
        /// </summary>
        /// <param name="id">The identifier of the link view parameter that is obtained or stored in database json</param>
        /// <param name="idProperty">stringified property of that expression will be added after the link and the model name (ex : '/[linkValue]/'+[ngModelName].[idProperty]</param>
        /// <param name="def">The default value for that link filled if the view parameter is empty</param>
        /// <returns></returns>
        public static string GetLink<T, TValue>(this IHtmlHelper<T> helper, string id, Expression<Func<T, TValue>> idProperty, string def = null)
        {
            var Provider = helper.GetService<IMoldsterGeneralHelper>();
            return Provider.GetLink(helper, id, idProperty, def);
        }

        /// <summary>
        /// Returns string presenting a link to another component, processed as view parameter
        /// </summary>
        /// <param name="id">The identifier of the link view parameter that is obtained or stored in database json</param>
        /// <param name="def">The default value for that link filled if the view parameter is empty</param>
        /// <param name="idProperty">if not null added after the link and the model name (ex : '/[linkValue]/'+[ngModelName].[idProperty]</param>
        /// <param name="idExpression">if not null added after the link (ex: '/[linkValue]/'+[idExpression]</param>
        /// <returns></returns>
        public static string GetLink(this IHtmlHelper helper, PageLink link)
        {
            var Provider = helper.GetService<IMoldsterGeneralHelper>();
            return Provider.GetLink(helper, link);
        }

        public static void AddBreadCrumbMoldster<T, TValue>(this IHtmlHelper<T> helper, string id, Expression<Func<T, TValue>> idProperty, string def = null, IHtmlContent title = null)
        {
            var Provider = helper.GetService<IMoldsterGeneralHelper>();

            var lnk = Provider.GetLink(helper, id, idProperty, def);
            if (lnk != null)
            {
                if (title == null)
                {
                    var pag = helper.GetViewParams().GetFromOther(id, def);
                    var pageId = RazorUtils.UrlToPageId(pag);
                    title = helper.Page(pageId);
                }
                helper.HeaderModel().BreadCrums.Add(new Models.BreadCrumbModel { Title = title, Link = "{{" + lnk + "}}" });
            }
        }

        public static void AddBreadCrumbMoldster<T>(this IHtmlHelper<T> helper, string identifier, string idExpression = null, string idProperty = null, string def = null, IHtmlContent title = null)
        {
            var Provider = helper.GetService<IMoldsterGeneralHelper>();

            var lnk = Provider.GetLink(helper, new PageLink(identifier, def, idExpression, idProperty));
            if (lnk != null)
            {
                if (title == null)
                {
                    var pag = helper.GetViewParams().GetFromOther(identifier, def);
                    var pageId = RazorUtils.UrlToPageId(pag);
                    title = helper.Page(pageId);
                }
                helper.HeaderModel().BreadCrums.Add(new Models.BreadCrumbModel { Title = title, Link = "{{" + lnk + "}}" });
            }
        }
    }
}
