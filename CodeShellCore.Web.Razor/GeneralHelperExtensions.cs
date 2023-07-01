using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using CodeShellCore.Text;
using CodeShellCore.Text.Localization;

using CodeShellCore.Web.Razor.Themes;
using CodeShellCore.Helpers;
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Types;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Web.Razor.Validation.Internal;
using CodeShellCore.Web.Razor.General;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Pages;

namespace CodeShellCore.Web.Razor
{
    public static class GeneralHelperExtensions
    {
        static ILocaleTextProvider TextProvider { get { return RazorConfig.LocaleTextProvider; } }
        //static IGeneralHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IGeneralHelper>(); } }

        internal static T GetService<T>(this IHtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.RequestServices.GetService<T>();
        }

        public static IHtmlHelper<T> For<T>(this IHtmlHelper helper, string ngModel, string formName)
        {
            return new CustomHelper<T>(helper, ngModel, formName);
        }

        public static int IntVal(this IHtmlHelper helper, Enum val)
        {
            return Convert.ToInt32(val);
        }

        public static long LongVal(this IHtmlHelper helper, Enum val)
        {
            return Convert.ToInt64(val);
        }



        public static Dictionary<string, object> EnumDictionary<T, T2>(this IHtmlHelper helper, bool reverse = false)
        {
            var values = Enum.GetValues(typeof(T));
            if (reverse)
                Array.Reverse(values);
            Dictionary<string, object> dic = new Dictionary<string, object>();

            foreach (Enum v in values)
            {
                dic[helper.EnumString(v)] = Convert.ChangeType(v, typeof(T2));
            }

            return dic;
        }




        #region Components
        public static IHtmlContent PageHeader<T>(this IHtmlHelper<T> helper, string addUrl = null, IHtmlContent addButton = null, IHtmlContent addButtonEmbedded = null)
        {
            var mod = helper.HeaderModel();

            if (addButton != null && addButtonEmbedded != null)
            {
                mod.AddButton = addButton;
                mod.EmbeddedAddButton = addButtonEmbedded;
            }
            else if (addUrl != null)
            {
                if (mod.AddButton == null)
                    mod.AddButton = helper.AddButton(addUrl);
                if (mod.EmbeddedAddButton == null)
                    mod.EmbeddedAddButton = helper.AddButton(addUrl, "buttonGra-sm");
            }

            return helper.GetComponent("PageHeader", mod);
        }
        public static IHtmlContent GetPartial(this IHtmlHelper helper, string template, object model = null, params object[] obs)
        {
            string st = Utils.CombineUrl("~/Views/" + template + ".cshtml");
            if (obs != null)
            {
                foreach (var ob in obs)
                    helper.SetViewData(ob);
            }

            var t = helper.PartialAsync(st, model);
            t.Wait();
            return t.Result;
        }

        public static IHtmlContent GetComponent(this IHtmlHelper helper, string template, object model = null)
        {
            string st = helper.GetTheme().GetTemplate(template);
            var t = helper.PartialAsync(st, model);
            t.Wait();
            return t.Result;
        }

        public static IHtmlContent GetPageTitle(this IHtmlHelper helper)
        {
            string id = helper.Config().PageIdentifier;
            if (id == null)
                return new HtmlString("");
            return helper.Page(id);
        }

        public static IHtmlContent BreadCrumbs(this IHtmlHelper helper, PageHeaderModel model)
        {
            return helper.GetComponent("Components/BreadCrumbs", model);
        }

        public static IHtmlContent Pagination<T>(this IHtmlHelper<T> helper, string srcName = null, int maxPages = 10)
        {
            PaginationModel m = new PaginationModel
            {
                Showing = srcName == null ? "options.Showing" : srcName + ".Opts.Showing",
                SelectEvent = srcName == null ? "PageSelected($event)" : srcName + ".PageChanged($event)",
                CurrentPage = srcName == null ? "pageIndex" : srcName + ".pageIndex",
                MaxPages = maxPages.ToString(),
                TotalCount = srcName == null ? "totalCount" : srcName + ".TotalCount"
            };

            return helper.GetComponent("Components/Pagination", m);
        }
        #endregion

        #region View Data
        public static string GetModelTypeName(this IHtmlHelper helper)
        {
            var data = helper.GetViewData<string>("ModelType");
            if (data == null)
            {
                data = helper.ViewData.ModelMetadata?.ModelType?.GetEntityName() ?? "";
                helper.SetViewData("ModelType", data);
            }
            return data;

        }
        public static PageOptions Config(this IHtmlHelper helper)
        {
            PageOptions conf = helper.GetViewData<PageOptions>();

            helper.GetModelTypeName();
            if (conf == null)
            {
                conf = new PageOptions();
                helper.SetViewData(conf);
            }
            else
            {
                if (conf.ViewParams.AddUrl != null)
                    conf.ViewParams.AddUrl = RazorUtils.ApplyConvension(conf.ViewParams.AddUrl, AppParts.Route);
                if (conf.ViewParams.EditUrl != null)
                    conf.ViewParams.EditUrl = RazorUtils.ApplyConvension(conf.ViewParams.EditUrl, AppParts.Route);
                if (conf.ViewParams.DetailsUrl != null)
                    conf.ViewParams.DetailsUrl = RazorUtils.ApplyConvension(conf.ViewParams.DetailsUrl, AppParts.Route);
                if (conf.ViewParams.ListUrl != null)
                    conf.ViewParams.ListUrl = RazorUtils.ApplyConvension(conf.ViewParams.ListUrl, AppParts.Route);
            }
            return conf;
        }

        public static ViewParams GetViewParams(this IHtmlHelper helper)
        {
            return helper.Config().ViewParams;
        }

        /// <summary>
        /// Gets an object from view data using <paramref name="index"/> and casts it to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="index"></param>
        /// <returns>null if no found</returns>
        public static T GetViewData<T>(this IHtmlHelper helper, string index = null) where T : class
        {
            object ob;
            index = index ?? typeof(T).Name;
            if (helper.ViewData.TryGetValue(index, out ob))
                return (T)ob;
            return null;
        }
        public static ViewDataDictionary GetDictionary(this IHtmlHelper helper)
        {

            ViewDataDictionary dic = new ViewDataDictionary(helper.ViewData);

            foreach (KeyValuePair<string, object> kv in helper.ViewData)
                dic[kv.Key] = kv.Value;

            return dic;
        }

        public static IHtmlContent Property<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> expression, string modelName = null)
        {
            modelName = modelName ?? helper.GetModelName();
            return new HtmlString(modelName + "." + RazorUtils.GetMemberName(expression));
        }

        public static void SetViewData(this IHtmlHelper helper, string key, object value)
        {
            helper.ViewData[key] = value;
        }

        public static void SetViewData(this IHtmlHelper helper, object data)
        {
            helper.ViewData[data.GetType().Name] = data;
        }

        public static void AddBreadCrumb<T>(this IHtmlHelper<T> helper, string url, IHtmlContent title = null)
        {
            var Provider = helper.GetService<IGeneralHelper>();

            var pageId = RazorUtils.UrlToPageId(url);
            var mod = new BreadCrumbModel
            {
                Title = title ?? helper.Page(pageId),
                Link = url
            };
            helper.HeaderModel().BreadCrums.Add(mod);
        }

        public static PageHeaderModel HeaderModel(this IHtmlHelper helper)
        {
            PageHeaderModel model = helper.GetViewData<PageHeaderModel>();

            if (model == null)
            {
                model = new PageHeaderModel
                {
                    Title = helper.GetPageTitle(),
                    AddButton = null,
                };

                if (helper.GetViewParams().ListUrl != null)
                {
                    string id = RazorUtils.UrlToPageId(helper.GetViewParams().ListUrl);
                    model.ListBreadCrumb = new BreadCrumbModel
                    {
                        Title = helper.Page(id),
                        Link = helper.GetViewParams().ListUrl
                    };
                }

                string addUrl = helper.GetViewParams().AddUrl;
                if (addUrl != null)
                {
                    model.AddButton = helper.AddButton(addUrl);
                    model.EmbeddedAddButton = helper.AddButton(addUrl, "buttonGra-sm");
                    model.IsListPage = true;
                }
                helper.SetViewData(model);
            }
            return model;
        }

        public static ModalModel ModalModel<T>(this IHtmlHelper<T> helper)
        {
            ModalModel model = helper.GetViewData<ModalModel>();

            if (model == null)
            {
                model = new ModalModel
                {
                    IsModal = true,
                    UseSearch = true,
                    ModalId = ""
                };

                helper.SetViewData(model);
            }
            return model;
        }



        #endregion

        #region Form model binding

        public static void SetNgModel(this IHtmlHelper helper, string name)
        {
            string index = "ModelName";
            if (helper is CustomHelper)
            {
                var h = helper as CustomHelper;
                index = $"c__{h.EntityTypeId}__" + index;
            }
            helper.ViewData[index] = name;
        }

        public static void SetNgForm(this IHtmlHelper helper, string name)
        {
            string index = "FormName";
            if (helper is CustomHelper)
            {
                var h = helper as CustomHelper;
                index = $"c__{h.EntityTypeId}__" + index;
            }
            helper.ViewData[index] = name;
        }

        public static string GetModelName(this IHtmlHelper helper)
        {
            string index = "ModelName";
            if (helper is CustomHelper)
            {
                var h = helper as CustomHelper;
                index = $"c__{h.EntityTypeId}__" + index;
            }
            if (helper.ViewData.TryGetValue(index, out object st))
                return st.ToString();

            return RazorConfig.ModelName;
        }

        public static string GetFormName(this IHtmlHelper helper)
        {
            string index = "FormName";
            if (helper is CustomHelper)
            {
                var h = helper as CustomHelper;
                index = $"c__{h.EntityTypeId}__" + index;
            }
            if (helper.ViewData.TryGetValue(index, out object st))
                return st.ToString();
            return RazorConfig.FormName;
        }

        #endregion

        #region Validation
        public static IValidationCollection VCollection(this IHtmlHelper helper)
        {
            return (IValidationCollection)Activator.CreateInstance(RazorConfig.ValidationCollectionType, helper.GetFormName());
        }

        public static IValidationCollection GetNumberValidators(this IHtmlHelper helper, bool required = false,
            int maxLen = 15,
            int minLen = 0,
            float? minVal = null,
            float maxVal = 0,
            string fieldName = null,
            string alternateLabel = null)
        {
            IValidationCollection coll = new ValidationCollection(helper.GetFormName());
            coll.AddNumeric();

            if (required)
                coll.AddRequired();

            if (minVal.HasValue || maxVal > 0)
                coll.AddMinMax(minVal, maxVal);

            if (minLen > 0 || maxLen > 0)
                coll.AddLength(minLen, maxLen);
            return coll;
        }
        public static IValidationCollection GetDateValidators(this IHtmlHelper helper, CalendarTypes type, bool required = false, DateRange range = null, string memberName = null, string alternateLabel = null)
        {
            IValidationCollection coll = helper.VCollection();
            coll.AddDate(type, range);
            if (required)
                coll.AddRequired();
            return coll;
        }
        #endregion

        #region UI Localization
        public static string EnumString(this IHtmlHelper helper, Enum t)
        {
            return t.GetString();
        }

        public static IHtmlContent Column(this IHtmlHelper helper, string id)
        {
            helper.AddText(StringType.Column, id);
            return helper.Raw(TextProvider.Column(id));
        }
        public static IHtmlContent Column<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp)
        {
            var id = RazorUtils.GetColumnId(exp);
            helper.AddText(StringType.Column, id);
            return helper.Raw(TextProvider.Column(id));
        }
        public static IHtmlContent Message(this IHtmlHelper helper, string id, params string[] paramateres)
        {
            helper.AddText(StringType.Message, id);
            return helper.Raw((TextProvider.Message(id, paramateres)));
        }

        public static IHtmlContent Word(this IHtmlHelper helper, string id)
        {
            helper.AddText(StringType.Word, id);
            return helper.Raw(TextProvider.Word(id));
        }

        public static IHtmlContent Word(this IHtmlHelper helper, string id, params string[] args)
        {
            helper.AddText(StringType.Word, id);
            return helper.Raw(TextProvider.Word(id, args));
        }

        public static IHtmlContent Word(this IHtmlHelper helper, Enum en)
        {
            helper.AddText(StringType.Word, en.GetString());
            return helper.Raw(TextProvider.Word(en));
        }
        public static IHtmlContent Page(this IHtmlHelper helper, string id)
        {
            helper.AddText(StringType.Page, id);
            return helper.Raw(TextProvider.Page(id));
        }

        internal static void AddText(this IHtmlHelper h, StringType type, string st)
        {
            var coll = General.Moldster.MoldsterGeneralHelperExtensions.CollectingData(h);
            if (coll)
            {
                var loc = h.GetViewData<LocalizationDataCollector>();
                if (loc != null)
                {
                    switch (type)
                    {
                        case StringType.Word:
                            loc.Words.Add(st);
                            break;
                        case StringType.Column:
                            loc.Columns.Add(st);
                            break;
                        case StringType.Page:
                            loc.Pages.Add(st);
                            break;
                        case StringType.Message:
                            loc.Messages.Add(st);
                            break;

                    }
                }

            }
        }

        #endregion

        #region Theme
        public static IRazorTheme GetTheme(this IHtmlHelper helper)
        {
            IRazorTheme th = helper.GetViewData<IRazorTheme>("Theme");
            if (th == null)
                return RazorConfig.Theme;
            return th;
        }

        public static void UseTheme<T>(this IHtmlHelper helper) where T : class, IRazorTheme
        {
            helper.ViewData["Theme"] = Activator.CreateInstance<T>();
        }

        public static void UseThemeConfig(this IHtmlHelper helper, Action<EditableTheme> conf)
        {
            var t = helper.GetTheme();
            var theme = new EditableTheme(t);
            conf(theme);
            helper.ViewData["Theme"] = theme;
        }

        public static void UseSplitTheme(this IHtmlHelper helper)
        {
            helper.ViewData["Theme"] = new SplitTheme();
        }

        public static void UserDefaultTheme(this IHtmlHelper helper)
        {
            helper.ViewData["Theme"] = RazorConfig.Theme;
        }
        #endregion

        #region Buttons

        public static string ButtonClass(this IHtmlHelper helper, BtnClass cls)
        {
            return RazorUtils.GetButtonClass(cls);
        }
        /// <summary>
        /// default submit button for default edit forms
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlContent SubmitButton(this IHtmlHelper helper)
        {
            return helper.GetComponent("Buttons/SubmitButton");
        }

        public static IHtmlContent TreeButton(this IHtmlHelper helper, string icon, string title, string function, object attr = null)
        {
            var mod = new LinkModel
            {
                Title = title,
                IconClass = icon,
                Function = function,
                Attrs = attr
            };
            return helper.GetComponent("Buttons/TreeButton", mod);
        }

        public static void AddHeaderButton(this IHtmlHelper helper,
            IHtmlContent content = null,
            string function = null,
            string url = null,
            BtnClass btn = BtnClass.Default,
            string icon = null,
            string identifier = null,
            string classes = null,
            string title = null,
            object attr = null)
        {
            var Provider = helper.GetService<IGeneralHelper>();
            Provider.AddHeaderButton(helper, content, function, url, btn, icon, identifier, classes, title, attr);
        }

        public static IHtmlContent AddButton(this IHtmlHelper helper, string url = null, string classes = null, IHtmlContent text = null)
        {
            url = url ?? helper.GetViewParams().AddUrl;
            url = (url[0] == '\'') ? url : "'" + url + "'";
            var modelType = helper.GetModelTypeName();
            return helper.Button(
                content: text ?? helper.Word("AddEntity", RazorConfig.LocaleTextProvider.Word(modelType)),
                classes: classes,
                url: url,
                btn: BtnClass.Success,
                attr: new { star__ngIf = "Permission.insert", automationId = "btnAdd" },
                icon: "fa fa-plus");
        }

        public static IHtmlContent Button(this IHtmlHelper helper,
            string text = null,
            string function = null,
            string url = null,
            BtnClass btn = BtnClass.Default,
            string icon = null,
            string identifier = null,
            IHtmlContent content = null,
            string classes = null,
            string title = null,
            object attr = null)
        {
            var Provider = helper.GetService<IGeneralHelper>();
            return Provider.Button(helper, text, function, url, btn, icon, identifier, content, classes, title, attr);

        }


        #endregion

        #region Containers

        public static IHtmlContent TabTitle(this IHtmlHelper helper, string containerId, string activationVariable, string textId = null, IHtmlContent titleContent = null, object attr = null)
        {
            var Provider = helper.GetService<IGeneralHelper>();
            return Provider.TabTitle(helper, containerId, activationVariable, textId, titleContent, attr);
        }

        /// <summary>
        /// writes the opening tag name and the closing tag name on the end bracket
        /// Should be used as 
        ///@using(var container=Html.Container(....))
        ///{
        ///    ...
        ///}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="tagName">html tag name</param>
        /// <param name="identifier"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static HtmlContainer Container<T>(this IHtmlHelper<T> helper, string tagName, string identifier, object attributes = null)
        {
            HtmlContainer tab = new HtmlContainer(helper, identifier, tagName, attributes);
            return tab;
        }

        /// <summary>
        /// writes a closing to tag
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static IHtmlContent CloseContainer(this IHtmlHelper helper, string tagName)
        {
            return new HtmlString($"</{tagName}>");
        }

        public static IHtmlContent SectionHeader(this IHtmlHelper helper, string textId = null, IHtmlContent content = null, string classes = "", object attrs = null)
        {
            var mod = new ContainerModel
            {
                TitleContent = content ?? helper.Word(textId),
                Attributes = attrs,
                TitleTextId = classes
            };

            return helper.GetComponent("Components/SectionHead", mod);
        }
        #endregion
    }
}
