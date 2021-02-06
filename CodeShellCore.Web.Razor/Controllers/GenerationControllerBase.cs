using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Configurator;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Controllers
{
    public class GenerationControllerBase : BaseApiController
    {

        IMoldsterService molds => GetService<IMoldsterService>();
        IBundlingService bundl => GetService<IBundlingService>();
        EnvironmentAccessor acc => GetService<EnvironmentAccessor>();
        IPublisherService pub => GetService<IPublisherService>();
        IPathsService paths => GetService<IPathsService>();
        IDataService data => GetService<IDataService>();
        ITemplateProcessingService c => GetService<ITemplateProcessingService>();
        IPreviewService prev => GetService<IPreviewService>();
        IScriptGenerationService sc => GetService<IScriptGenerationService>();
        IScriptModelMappingService map => GetService<IScriptModelMappingService>();

        public IActionResult ProcessForPage([FromQuery]MoldsterRequest req)
        {
            if (req.PageId == null)
                Respond(new { Message = "PageId is required" }, System.Net.HttpStatusCode.BadRequest);

            SubmitResult = molds.ProcessForPage(req.PageId.Value);
            return Respond();
        }

        public IActionResult RenderPage(long id)
        {
            var dto = new PageRenderDTO { Id = id };
            molds.RenderPage(null, dto);
            sc.GenerateModuleDefinitionByPage(dto);
            SubmitResult = new Data.Helpers.SubmitResult(0, "success_message");
            return Respond();
        }

        [HttpPost]
        public IActionResult Render([FromBody]RenderDTO dto)
        {
            SubmitResult = molds.RenderDomainModule(dto);
            return Respond();
        }

        [HttpPost]
        public IActionResult Process([FromBody]RenderDTO dto)
        {
            molds.ProcessTemplates(dto.Mod, dto.NameChain);

            return Respond();
        }

        public IActionResult CollectTemplateData(long id)
        {
            c.CollectTemplateData(id);
            return Respond();
        }

        public IActionResult RenderTenant([FromBody]RenderDTO dto)
        {
            SubmitResult = molds.RenderAll(dto.Mod);
            return Respond();
        }

        public IActionResult SyncTenants([FromBody]AssociateDTO dto)
        {
            SyncResult res = molds.SyncTenants(dto.Id1, dto.Id2);
            SubmitResult = new SubmitResult();
            SubmitResult.Data["Sync"] = res;
            return Respond();
        }

        public IActionResult ModuleDefinition([FromBody]RenderDTO dto)
        {
            molds.RenderModuleDefinition(dto.Mod);
            return Respond();
        }

        public IActionResult Mapping()
        {
            map.ScriptMapping();
            return Respond();
        }

        private static void OnTaskCompleted(BundlingTask tsk, bool success, string message, IOutputWriter output = null)
        {
            using (var sc = Shell.GetScope())
            {
                if (success)
                {
                    var _acc = sc.ServiceProvider.GetService<EnvironmentAccessor>();
                    var _paths = sc.ServiceProvider.GetService<IPathsService>();
                    _acc.CurrentEnvironment = _paths.GetEnvironments().Where(d => d.Name == tsk.Environment).FirstOrDefault();


                    var _bund = sc.ServiceProvider.GetService<IBundlingService>();
                    var _pub = sc.ServiceProvider.GetService<IPublisherService>();
                    if (output != null)
                    {
                        _bund.OutputWriter = output;
                        _pub.OutputWriter = output;
                    }
                    string version = _bund.GetAppVersion(tsk.TenantCode, true);
                    _pub.UploadTenantBundle(tsk.TenantCode, "v" + version);
                    _pub.SetTenantInfo(tsk.TenantCode, version);
                }
                BundlingTask.ClearCompleted();
                if (tsk.Status != "NULL")
                {
                    var pusher = sc.ServiceProvider.GetService<IMessagePusher<IBundlingTasksNotifications>>();
                    pusher.Publish(d => d.TaskChanged(tsk));
                }

            }

        }

        public IActionResult StopPreview()
        {
            var res = prev.StopPreview();
            return Respond(res);
        }

        public IActionResult GetActivePreview()
        {
            if (prev.CurrentPreview != null)
                return Respond(new PreviewData
                {
                    TenantCode = prev.CurrentPreview.TenantCode,
                    Url = Utils.CombineUrl(paths.UIUrl, prev.CurrentPreview.TenantCode)
                });
            else
                return Respond();
        }

        public IActionResult StartTenantPreview([FromBody]DbCreationRequest req)
        {
            acc.CurrentEnvironment = new MoldsterEnvironment { Upload = new Net.UploadConfig { Type = "DEV" } };
            pub.SetTenantInfo(req.TenantCode, null);
            var res = prev.StartPreview(req.TenantCode, paths.UILaunchProfile);
            return Respond(res);
        }

        string increment(string ver)
        {
            string[] spl = ver.Split('.');
            spl[3] = (int.Parse(spl[3]) + 1).ToString();
            return string.Join(".", spl);
        }

        public IActionResult PublishTenant([FromBody]DbCreationRequest req)
        {
            SubmitResult = new SubmitResult();
            var outwriter = GetService<IOutputWriter>();
            var oth = data.GetAppCodes().Where(d => d != req.TenantCode);

            var ver = bundl.GetAppVersion(req.TenantCode, true);
            if (req.Force ?? false)
            {
                ver = increment(ver);
            }
            BundlingTask.ClearCompleted();

            var tsk = BundlingTask.GetTask(req.TenantCode, ver);
            if (tsk == null)
            {
                if (bundl.StartProductionPackIfNeeded(req.TenantCode, out tsk, ver))
                {
                    tsk.Environment = req.Environment;

                    tsk.OnComplete = (t, res) => OnTaskCompleted(tsk, res.IsSuccess, res.Message, outwriter);
                    SubmitResult.Message = "started_new_task";
                    BundlingTask.Add(tsk);
                }
                else
                {
                    OnTaskCompleted(new BundlingTask
                    {
                        Status = "NULL",
                        TenantCode = req.TenantCode,
                        Version = ver,
                        Environment = req.Environment
                    }, true, "already_rendered");
                    SubmitResult.Message = "already_rendered";
                }

            }
            else
            {
                SubmitResult.Code = 1;
                SubmitResult.Message = "task_is_already_running";
            }
            SubmitResult.Data["Task"] = tsk;
            return Respond();

        }


    }
}
