using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Builder.Dtos;

namespace CodeShellCore.Moldster.Builder.Services
{
    public interface IPreviewService
    {
        Result StartPreview(string tenantCode, string launchProfile = null);
        Result StopPreview();
        PreviewTask CurrentPreview { get; }
    }
}
