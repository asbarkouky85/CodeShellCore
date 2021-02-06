using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IMoldProvider
    {
        string BootMold { get; }
        string GetBaseComponentMold(bool serviced);
        string GetDomainModuleMold();
        string ComponentMold { get; }
        string MainComponentMold { get; }
        string LocaleLoaderMold { get; }
        string AppModuleMold { get; }
        string ParentRouteMold { get; }
        string RouteMold { get; }
        string RoutesMold { get; }
        string ServiceMold { get; }
        string SharedModuleMold { get; }
        string DevWebpackConfigMold { get; }
        string ProWebpackConfigMold { get; }
        string ModuleTsConfigMold { get; }
        string BasicComponent { get; }
        string LookupComponent { get; }
        string BaseModuleMold { get; }
    }
}
