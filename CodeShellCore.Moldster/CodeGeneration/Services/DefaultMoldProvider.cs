using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public class DefaultMoldProvider : IMoldProvider
    {

        private string baseComp;
        private string servicedBaseComp;

        private string domModule_lazy;

        public DefaultMoldProvider()
        {
            baseComp = Encoding.UTF8.GetString(Properties.Resources.BaseComponent_ts);
            servicedBaseComp = Encoding.UTF8.GetString(Properties.Resources.ServicedBaseComponent_ts);
            domModule_lazy = Encoding.UTF8.GetString(Properties.Resources.DomainModule_Lazy_ts);

            ComponentMold = Encoding.UTF8.GetString(Properties.Resources.Component_ts);
            LocaleLoaderMold = Encoding.UTF8.GetString(Properties.Resources.LocaleLoader_ts);
            AppModuleMold = Encoding.UTF8.GetString(Properties.Resources.Module_ts);
            ParentRouteMold = Encoding.UTF8.GetString(Properties.Resources.ParentRoute_ts);
            RouteMold = Encoding.UTF8.GetString(Properties.Resources.Route_ts);
            RoutesMold = Encoding.UTF8.GetString(Properties.Resources.Routes_ts);
            ServiceMold = Encoding.UTF8.GetString(Properties.Resources.Service_ts);
            SharedModuleMold = Encoding.UTF8.GetString(Properties.Resources.SharedModule_ts);
            BootMold = Encoding.UTF8.GetString(Properties.Resources.Boot_ts);
            MainComponentMold = Encoding.UTF8.GetString(Properties.Resources.AppComponent_ts);
            BasicComponent = Encoding.UTF8.GetString(Properties.Resources.BasicComponent_ts);
            BaseModuleMold = Encoding.UTF8.GetString(Properties.Resources.BaseModule_ts);
            AngularJson = Encoding.UTF8.GetString(Properties.Resources.angular_json);
            AngularJsonProject = Encoding.UTF8.GetString(Properties.Resources.angular_json_project);
            ServerConfigMold = Encoding.UTF8.GetString(Properties.Resources.server_config_ts);
        }

        public virtual string BootMold { get; private set; }
        public virtual string ComponentMold { get; private set; }

        public virtual string LocaleLoaderMold { get; private set; }

        public virtual string AppModuleMold { get; private set; }

        public virtual string ParentRouteMold { get; private set; }

        public virtual string RouteMold { get; private set; }

        public virtual string RoutesMold { get; private set; }

        public virtual string ServiceMold { get; private set; }

        public virtual string SharedModuleMold { get; private set; }

        //public virtual string ModuleTsConfigMold { get; private set; }

        public virtual string MainComponentMold { get; private set; }

        public virtual string BasicComponent { get; private set; }

        public virtual string LookupComponent { get; private set; }

        public virtual string BaseModuleMold { get; private set; }

        public string AngularJson { get; private set; }

        public string AngularJsonProject { get; private set; }

        public string ServerConfigMold { get; private set; }

        public virtual string GetBaseComponentMold(bool serviced)
        {
            return serviced ? servicedBaseComp : baseComp;
        }

        public virtual string GetDomainModuleMold()
        {
            return domModule_lazy;
        }
    }
}
