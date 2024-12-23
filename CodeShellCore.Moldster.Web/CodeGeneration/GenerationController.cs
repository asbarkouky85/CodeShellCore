using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Notifications;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Razor.CodeGeneration
{
    public class GenerationController : BaseApiController
    {
        IMoldsterService molds => GetService<IMoldsterService>();
        IBundlingService bundl => GetService<IBundlingService>();
        EnvironmentAccessor acc => GetService<EnvironmentAccessor>();
        IPublisherService pub => GetService<IPublisherService>();
        IPathsService paths => GetService<IPathsService>();
        IDataService data => GetService<IDataService>();
        IPageCategoryHtmlService c => GetService<IPageCategoryHtmlService>();
        IPreviewService prev => GetService<IPreviewService>();
        IDomainScriptGenerationService sc => GetService<IDomainScriptGenerationService>();

        [HttpGet]
        public async Task<SubmitResult> ProcessForPage([FromQuery] MoldsterRequest req)
        {
            if (req.PageId == null)
                return new SubmitResult { Message = "PageId is required", Code = 400 };
            return await molds.ProcessForPage(req.PageId.Value);
        }

        [HttpGet]
        public async Task<SubmitResult> RenderPage(long id)
        {
            var dto = new PageRenderDTO { Id = id };
            await molds.RenderPage(null, dto);
            await sc.GenerateModuleDefinitionByPage(dto);
            SubmitResult = new SubmitResult(0, "success_message");
            return SubmitResult;
        }

        [HttpPost]
        public async Task<SubmitResult> Render([FromBody] RenderDTO dto)
        {
            return await molds.RenderDomainModule(dto);
        }

        [HttpPost]
        public async Task<SubmitResult> Process([FromBody] RenderDTO dto)
        {
            await molds.ProcessTemplates(dto.Mod, dto.NameChain);

            return SubmitResult;
        }

        public async Task<SubmitResult> CollectTemplateData(long id)
        {
            await c.CollectTemplateData(id);
            return SubmitResult;
        }

        public async Task<SubmitResult> RenderTenant([FromBody] RenderDTO dto)
        {
            SubmitResult = await molds.RenderAll(dto.Mod);
            return SubmitResult;
        }

        public async Task<SubmitResult> SyncTenants([FromBody] AssociateDTO dto)
        {
            SyncResult res = await molds.SyncTenants(dto.Id1, dto.Id2);
            SubmitResult = new SubmitResult();
            SubmitResult.Data["Sync"] = res;
            return SubmitResult;
        }

        public async Task<SubmitResult> ModuleDefinition([FromBody] RenderDTO dto)
        {
            await molds.RenderModuleDefinition(dto.Mod);
            return SubmitResult;
        }

        private static async Task OnBundlingTaskCompleted(BundlingTask tsk, bool success, string message, IOutputWriter output = null)
        {
            using (var sc = Shell.GetScope())
            {
                if (success)
                {
                    var _acc = sc.ServiceProvider.GetService<EnvironmentAccessor>();
                    var _paths = sc.ServiceProvider.GetService<IPathsService>();
                    _acc.CurrentEnvironment = _paths.GetEnvironmentByName(tsk.Environment);


                    var _bund = sc.ServiceProvider.GetService<IBundlingService>();
                    var _pub = sc.ServiceProvider.GetService<IPublisherService>();
                    if (output != null)
                    {
                        _bund.OutputWriter = output;
                        _pub.OutputWriter = output;
                    }
                    string version = await _bund.GetAppVersion(tsk.TenantCode, true);
                    await _pub.UploadTenantBundle(tsk.TenantCode, "v" + version);
                    await _pub.SetTenantInfo(tsk.TenantCode, version);
                }
                BundlingTask.ClearCompleted();
                if (tsk.Status != "NULL")
                {
                    var pusher = sc.ServiceProvider.GetService<IEmitter<IBundlingTasksNotifications>>();
                    pusher.Emit(d => d.TaskChanged(tsk));
                }

            }

        }

        public IActionResult StopPreview()
        {
            var res = prev.StopPreview();
            return Respond(res);
        }

        public Task<PreviewData> GetActivePreview()
        {

            if (prev.CurrentPreview != null)
                return Task.FromResult(new PreviewData
                {
                    TenantCode = prev.CurrentPreview.TenantCode,
                    Url = Utils.CombineUrl(paths.UIUrl, prev.CurrentPreview.TenantCode)
                });
            else
                return null;
        }

        public IActionResult StartTenantPreview([FromBody] DbCreationRequest req)
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

        public async Task<SubmitResult> PublishTenant([FromBody] DbCreationRequest req)
        {
            SubmitResult = new SubmitResult();
            var outwriter = GetService<IOutputWriter>();
            var oth = (await data.GetAppCodes()).Where(d => d != req.TenantCode);

            var ver = await bundl.GetAppVersion(req.TenantCode, true);
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

                    tsk.OnComplete = (res) => OnBundlingTaskCompleted(tsk, res.IsSuccess, res.Message, outwriter);
                    SubmitResult.Message = "started_new_task";
                    BundlingTask.Add(tsk);
                }
                else
                {
                    await OnBundlingTaskCompleted(new BundlingTask
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
            return SubmitResult;

        }


    }
}
