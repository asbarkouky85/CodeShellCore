using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Http;
using CodeShellCore.Files.Logging;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Localization;

namespace CodeShellCore.Web.Razor.Services
{
    public class RazorRenderingService : ServiceBase, IMoldsterRazorRenderingService
    {
        protected IRazorViewEngine _razorViewEngine;
        protected ITempDataProvider _tempDataProvider;
        public RazorRenderingService(IRazorViewEngine engine, ITempDataProvider tmp)
        {
            _razorViewEngine = engine;
            _tempDataProvider = tmp;
        }

        public TemplateDataCollector GetCollector(HttpContext context, string viewName, string layout = null)
        {
            ActionContext actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                    throw new Exception($"{viewName} does not match any available view");

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

                viewDictionary["CollectViewData"] = true;
                viewDictionary["PageControls"] = new TemplateDataCollector
                {
                    EntityName = null,
                    Controls = new List<ControlDTO>()
                };
                viewDictionary["LocalizationDataCollector"] = new LocalizationDataCollector();


                if (layout != null)
                {
                    viewDictionary["PageOptions"] = new PageOptions
                    {
                        Layout = layout
                    };
                }

                var viewContext = new ViewContext(
                            actionContext,
                            viewResult.View,
                            viewDictionary,
                            new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                            sw,
                            new HtmlHelperOptions()
                        );

                var t = RenderAsync(viewResult, viewContext);
                t.Wait();
                if (t.Result.Code != 200)
                {
                    throw new CodeShellHttpException(t.Result);
                }

                var data = (TemplateDataCollector)viewContext.ViewData["PageControls"];
                data.Localization = ((LocalizationDataCollector)viewContext.ViewData["LocalizationDataCollector"]);
                data.Localization.Unify();
                return data;
            }
        }

        public string RenderPartial(HttpContext context, string viewName, object model = null, Dictionary<string, object> viewData = null)
        {
            ActionContext actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());


            using (var sw = new StringWriter())
            {
                using (var c = SW.Measure())
                {
                    
                    ViewEngineResult viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
                    
                    if (viewResult.View == null)
                        throw new Exception($"{viewName} does not match any available view");

                    var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
                    
                    if (model != null)
                        viewDictionary.Model = model;

                    if (viewData != null)
                    {
                        foreach (var item in viewData)
                            viewDictionary[item.Key] = item.Value;
                    }

                    var opts = new HtmlHelperOptions();

                    var viewContext = new ViewContext(
                        actionContext,
                        viewResult.View,
                        viewDictionary,
                        new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                        sw,
                        opts
                    );
                    
                    var t = RenderAsync(viewResult, viewContext);
                    t.Wait();
                    if (t.Result.Code != 200)
                    {
                        throw new CodeShellHttpException(t.Result);
                    }
                    return sw.ToString();
                }
            }
        }

        protected async Task<HttpResult> RenderAsync(ViewEngineResult res, ViewContext viewContext)
        {
            try
            {
                await res.View.RenderAsync(viewContext);
                return new HttpResult();
            }
            catch (Exception ex)
            {
                var x = new HttpResult(HttpStatusCode.InternalServerError);
                x.Message = "Error Rendering View ( ~" + viewContext.View.Path + ")";
                x.SetException(ex, true);
                return x;
            }
        }

        public string Render(HttpContext context, string layout, string viewName, object model = null, Dictionary<string, object> viewData = null)
        {
            ActionContext actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                RazorPageResult viewResult = _razorViewEngine.FindPage(actionContext, viewName);


                viewResult.Page.Layout = layout;
                if (viewResult.Page == null)
                    throw new Exception($"{viewName} does not match any available view");

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

                if (model != null)
                    viewDictionary.Model = model;

                if (viewData != null)
                {
                    foreach (var item in viewData)
                        viewDictionary[item.Key] = item.Value;
                }

                var opts = new HtmlHelperOptions();

                var viewContext = new ViewContext();
                //var viewContext = new ViewContext(
                //    actionContext,
                //    null,
                //    viewDictionary,
                //    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                //    sw,
                //    opts
                //);


                viewResult.Page.ViewContext = new ViewContext();
                viewResult.Page.ViewContext.View.RenderAsync(viewContext);

                Task.WaitAll(viewResult.Page.ExecuteAsync());
                viewResult.Page.ViewContext.ViewData = viewDictionary;
                viewResult.Page.ViewContext.Writer = sw;
                Task.WaitAll(viewResult.Page.ViewContext.View.RenderAsync(viewContext));

                return sw.ToString();
            }
        }
    }
}
