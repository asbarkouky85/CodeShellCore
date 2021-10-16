using CodeShellCore.Moldster.Configurator.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Configurator
{
    public interface IBundlingTasksNotifications
    {
        Task TaskChanged(BundlingTask count);
    }
}
