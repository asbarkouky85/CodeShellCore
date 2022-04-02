using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.Builder
{
    public interface IPreviewService
    {
        Result StartPreview(string tenantCode, string launchProfile = null);
        Result StopPreview();
        PreviewTask CurrentPreview { get; }
    }
}
