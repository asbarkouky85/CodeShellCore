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
using CodeShellCore.Moldster.Db.Razor;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Web.Razor.Validation.Internal;
using CodeShellCore.Web.Razor.General;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Razor
{
    public static class GeneralHelpers
    {
        static ILocaleTextProvider TextProvider { get { return RazorConfig.LocaleTextProvider; } }
        static IGeneralHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IGeneralHelper>(); } }

        public static PageOptions Config(this IHtmlHelper helper)
        {
            PageOptions conf = helper.GetViewData<PageOptions>();
            if (conf == null)
            {
                conf = new PageOptions();
                helper.SetViewData(conf);
            }
            return conf;
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

        public static string ButtonClass(this IHtmlHelper helper, BtnClass cls)
        {
            return RazorUtils.GetButtonClass(cls);
        }


        public static IHtmlContent GetPartial(this IHtmlHelper helper, string template, object model = null, params object[] obs)
        {
            string st = Utils.CombineUrl("~/Views/" + template + ".cshtml");
            if (obs != null)
            {
                foreach (var ob in obs)
                    helper.SetViewData(ob);
            }
            return helper.Partial(st, model);
        }

        public static IHtmlContent GetComponent(this IHtmlHelper helper, string template, object model = null)
        {
            string st = helper.GetTheme().GetTemplate(template);
            return helper.Partial(st, model);
        }

        public static IHtmlContent GetPageTitle(this IHtmlHelper helper)
        {
            string id = helper.Config().PageIdentifier;
            if (id == null)
                return new HtmlString("");
            return helper.Page(id);
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

            return helper.GetComponent("Pagination", m);
        }

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

        #region View Data

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

        public static PageHeaderModel HeaderModel<T>(this IHtmlHelper<T> helper)
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
                    model.BreadCrums[id] = helper.GetViewParams().ListUrl;
                }
                string addUrl = helper.GetViewParams().AddUrl;
                if (addUrl != null)
                {
                    model.AddButton = helper.AddButton(addUrl);
                    model.EmbeddedAddButton = helper.AddButton(addUrl, "buttonGra-sm");
                    model.IsListPage = true;
                }
                helper.SetViewData("HeaderModel", model);
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
                    Width = 600,
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
            return helper.Raw(TextProvider.Column(id));
        }
        public static IHtmlContent Column<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp)
        {
            return helper.Raw(TextProvider.Column(RazorUtils.GetColumnId(exp)));
        }
        public static IHtmlContent Message(this IHtmlHelper helper, string id, params string[] paramateres)
        {
            return helper.Raw((TextProvider.Message(id, paramateres)));
        }

        public static IHtmlContent Word(this IHtmlHelper helper, string id)
        {
            return helper.Raw(TextProvider.Word(id));
        }

        public static IHtmlContent Word(this IHtmlHelper helper, string id, params string[] args)
        {
            return helper.Raw(TextProvider.Word(id, args));
        }

        public static IHtmlContent Word(this IHtmlHelper helper, Enum en)
        {
            return helper.Raw(TextProvider.Word(en));
        }
        public static IHtmlContent Page(this IHtmlHelper helper, string id)
        {
            return helper.Raw(TextProvider.Page(id));
        }

        public static HtmlString HiddenMessage(this IHtmlHelper helper, string id, params string[] parameters)
        {
            string mes = TextProvider.Message(id, parameters);
            string str = "<div style = \"display: none\" id = \"msg__" + id + "\" >" + mes + "</div>";

            return new HtmlString(str);
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

        public static IHtmlContent GetAddEntityString<T>(this IHtmlHelper<T> helper)
        {
            var s = helper.ViewData.ModelMetadata.ModelType.RealModelType().Name;
            string modelType = helper.GetViewParams().ModelType ?? s;

            return helper.Word("AddEntity", RazorConfig.LocaleTextProvider.Word(modelType));
        }

        public static IHtmlContent AddButton<T>(this IHtmlHelper<T> helper, string url = null, string classes = null, IHtmlContent text = null)
        {
            url = url ?? helper.GetViewParams().AddUrl;
            url = (url[0] == '\'') ? url : "'" + url + "'";
            return helper.Button(
                content: text ?? helper.GetAddEntityString(),
                classes: classes,
                url: url,
                btn: BtnClass.Success,
                attr: new { star__ngIf = "Permission.insert" },
                icon: "glyphicon glyphicon-plus");
        }

        /// <summary>
        /// write a button tag according to parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="text">if not null text is translated using Word()</param>
        /// <param name="function">full function name and arguments</param>
        /// <param name="url">link to go to on click</param>
        /// <param name="btn">button class</param>
        /// <param name="icon">fa or glyphicon class, if not null an 'i' tag is added</param>
        /// <param name="identifier"></param>
        /// <param name="content"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static IHtmlContent Button<T>(this IHtmlHelper<T> helper,
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
            return Provider.Button(helper, text, function, url, btn, icon, identifier, content, classes, title, attr);
            
        }
        #endregion

        #region Containers

        public static IHtmlContent TabTitle(this IHtmlHelper helper, string containerId, string activationVariable, string textId = null, object attr = null)
        {
            return Provider.TabTitle(helper, containerId, activationVariable, textId, attr);
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
        #endregion
    }
}
