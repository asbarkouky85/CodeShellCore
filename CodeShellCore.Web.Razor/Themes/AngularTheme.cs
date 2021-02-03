using CodeShellCore.Helpers;

namespace CodeShellCore.Web.Razor.Themes
{
    public class AngularTheme : DefaultTheme, IRazorTheme
    {
        public override string BasePath { get { return "~/ShellComponents/Angular"; } }   
    }
}
