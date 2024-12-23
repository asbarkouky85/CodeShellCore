using CodeShellCore.Helpers;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public interface IPreviewService
    {
        Task<Result> StartPreview(string tenantCode, string launchProfile = null);
        Task<Result> StopPreview();
        PreviewTask CurrentPreview { get; }
    }
}
