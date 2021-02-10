using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface IPreviewService
    {
        Result StartPreview(string tenantCode, string launchProfile = null);
        Result StopPreview();
        PreviewTask CurrentPreview { get; }
    }
}
