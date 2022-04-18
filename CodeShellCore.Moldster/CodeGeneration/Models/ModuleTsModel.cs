namespace CodeShellCore.Moldster.CodeGeneration.Models
{
    public class ModuleTsModel
    {
        public string Code { get; set; }
        public string Declarations { get; set; }
        public string Modules { get; set; }
        public string ModuleImports { get; set; }
        public string MainComponentPath { get; set; }
        public string MainComponentName { get; set; }
        public string BaseAppModuleName { get; set; }
        public string BaseAppModulePath { get; set; }
        public string RoutesModulePath { get; set; }
        public string BaseName { get; set; }
        public string BaseHref { get; set; }
        public string Registrations { get; set; }
        public string Version { get; set; }
    }
}
