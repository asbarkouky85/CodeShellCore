using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Services.Http;

namespace CodeShellCore.Moldster.Razor.Services
{
    public class MoldsterRazorService : RazorRenderingService, IMoldsterRazorService
    {
        public MoldsterRazorService(IRazorViewEngine engine, ITempDataProvider tmp) : base(engine, tmp)
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
                viewDictionary["PageControls"] = new TemplateDataCollector();
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

                return (TemplateDataCollector)viewContext.ViewData["PageControls"];
            }
        }

    }
}
