using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Properties;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Configurator.Commander.Service
{
    public class ConfigMoldProvider : IMoldProvider
    {
        DefaultMoldProvider _def;

        private string baseComp;
        private string servicedBaseComp;
        private string domModule;
        private string domModule_lazy;

        public ConfigMoldProvider()
        {
            _def = new DefaultMoldProvider();

            baseComp = Encoding.UTF8.GetString(CodeShellCore.Moldster.Properties.Resources.BaseComponent_ts);
            servicedBaseComp = Encoding.UTF8.GetString(CodeShellCore.Moldster.Properties.Resources.ServicedBaseComponent_ts);
            domModule = Encoding.UTF8.GetString(CodeShellCore.Moldster.Properties.Resources.DomainModule_ts);
            domModule_lazy = Encoding.UTF8.GetString(CodeShellCore.Moldster.Properties.Resources.DomainModule_Lazy_ts);
        }

        public string BootMold => _def.BootMold;

        public string ComponentMold => _def.ComponentMold;

        public string MainComponentMold => _def.MainComponentMold;

        public string LocaleLoaderMold => _def.LocaleLoaderMold;

        public string AppModuleMold => _def.AppModuleMold;

        public string ParentRouteMold => _def.ParentRouteMold;

        public string RouteMold => _def.RouteMold;

        public string RoutesMold => _def.RoutesMold;

        public string ServiceMold => GetService();

        public string SharedModuleMold => _def.SharedModuleMold;

        public string DevWebpackConfigMold => _def.DevWebpackConfigMold;

        public string ProWebpackConfigMold => _def.ProWebpackConfigMold;

        public string ModuleTsConfigMold => _def.ModuleTsConfigMold;

        public string BasicComponent => _def.BasicComponent;

        public string LookupComponent => _def.LookupComponent;

        public string BaseModuleMold => _def.BaseModuleMold;

        public string GetBaseComponentMold(bool serviced)
        {
            return serviced ? servicedBaseComp : baseComp;
        }

        public string GetDomainModuleMold(bool lazy)
        {
            return lazy ? domModule_lazy : domModule;
        }

        public string GetService()
        {
            return Encoding.UTF8.GetString(Properties.Resources.ConfigService_ts);
        }
    }
}
