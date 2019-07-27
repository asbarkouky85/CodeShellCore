using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class DefaultMoldProvider : IMoldProvider
    {
        readonly PathProvider Paths;
        private string baseComp;
        private string servicedBaseComp;
        private string domModule;
        private string domModule_lazy;

        public DefaultMoldProvider(PathProvider prov)
        {
            Paths = prov;
            baseComp = Encoding.UTF8.GetString(Properties.Resources.BaseComponent_ts);
            servicedBaseComp = Encoding.UTF8.GetString(Properties.Resources.ServicedBaseComponent_ts);
            domModule = Encoding.UTF8.GetString(Properties.Resources.DomainModule_ts);
            domModule_lazy = Encoding.UTF8.GetString(Properties.Resources.DomainModule_Lazy_ts);

            ComponentMold = Encoding.UTF8.GetString(Properties.Resources.Component_ts);
            LocaleLoaderMold = Encoding.UTF8.GetString(Properties.Resources.LocaleLoader_ts);
            AppModuleMold = Encoding.UTF8.GetString(Properties.Resources.Module_ts);
            ParentRouteMold = Encoding.UTF8.GetString(Properties.Resources.ParentRoute_ts);
            RouteMold = Encoding.UTF8.GetString(Properties.Resources.Route_ts);
            RoutesMold = Encoding.UTF8.GetString(Properties.Resources.Routes_ts);
            ServiceMold = Encoding.UTF8.GetString(Properties.Resources.Service_ts);
            SharedModuleMold = Encoding.UTF8.GetString(Properties.Resources.SharedModule_ts);
            DevWebpackConfigMold = Encoding.UTF8.GetString(Properties.Resources.webpack_config_js);
            ProWebpackConfigMold = Encoding.UTF8.GetString(Properties.Resources.webpack_tenant_js);
            ModuleTsConfigMold = Encoding.UTF8.GetString(Properties.Resources.webpack_tenant_js_json);
            BootMold= Encoding.UTF8.GetString(Properties.Resources.Boot_ts);
            MainComponentMold= Encoding.UTF8.GetString(Properties.Resources.AppComponent_ts);
            BasicComponent = Encoding.UTF8.GetString(Properties.Resources.BasicComponent_ts);
            BaseModuleMold = Encoding.UTF8.GetString(Properties.Resources.BaseModule_ts);
        }

        public string BootMold { get; private set; }
        public string ComponentMold { get; private set; }

        public string LocaleLoaderMold { get; private set; }

        public string AppModuleMold { get; private set; }

        public string ParentRouteMold { get; private set; }

        public string RouteMold { get; private set; }

        public string RoutesMold { get; private set; }

        public string ServiceMold { get; private set; }

        public string SharedModuleMold { get; private set; }

        public string DevWebpackConfigMold { get; private set; }

        public string ProWebpackConfigMold { get; private set; }

        public string ModuleTsConfigMold { get; private set; }

        public string MainComponentMold { get; private set; }

        public string BasicComponent { get; private set; }

        public string LookupComponent { get; private set; }

        public string BaseModuleMold { get; private set; }

        public string GetBaseComponentMold(bool serviced)
        {
            return serviced ? servicedBaseComp : baseComp;
        }

        public string GetDomainModuleMold(bool lazy)
        {
            return lazy ? domModule_lazy : domModule;
        }
    }
}
