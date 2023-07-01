using CodeShellCore.Data;
using CodeShellCore.Localization;
using CodeShellCore.Text;

namespace CodeShellCore.Moldster.Pages
{
    [EntityName("Page")]
    public class PageDetailsDto 
    {
        public long TenantId { get; set; }
        public string TenantCode { get; set; }
        public PageDto Page { get; set; }
        public string BaseViewPath { get; set; }
        public string PageIdentifier { get; set; }
        public string ActionName { get; set; }
        public string ResourceName { get; set; }
        public string DomainName { get; set; }
        public bool ParentHasResource { get; set; }
        public string CollectionId { get; set; }

        public string ComponentName { get { return Page.ViewPath.GetAfterLast("/"); } }
        public string Registration { get { return "Registry.Register(\"" + Page.ViewPath + "\", " + ComponentName + ");\n"; } }

        public string GetImportString(bool sameFolder = false)
        {
            string path = !sameFolder ? "../" + Page.ViewPath : "./" + ComponentName;
            return $"import {{ {ComponentName} }} from \"{path}\";\n";
        }


    }
}
