using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public interface ILegacyBundlingService
    {
        Task WriteWebpackConfigFiles();
    }
}
