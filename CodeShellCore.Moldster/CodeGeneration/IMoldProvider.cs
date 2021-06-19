using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IMoldProvider
    {

        string GetBaseComponentMold(bool serviced);
        string GetDomainModuleMold();
        string AppModuleMold { get; }
        string BaseModuleMold { get; }
        string ServerConfigMold { get; }
        string BasicComponent { get; }
        string BootMold { get; }
        string ComponentMold { get; }
        string DevWebpackConfigMold { get; }
        string LocaleLoaderMold { get; }
        string LookupComponent { get; }
        string MainComponentMold { get; }
        string ModuleTsConfigMold { get; }
        string ParentRouteMold { get; }
        string ProWebpackConfigMold { get; }
        string RouteMold { get; }
        string RoutesMold { get; }
        string ServiceMold { get; }
        string SharedModuleMold { get; }
        string AngularJson { get; }
        string AngularJsonProject { get; }
    }
}
