using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Features
{
    public interface IFeatureConfiguration
    {
        void BlockAll();
        void BlockDomains(string[] domains);
        void BlockServices(string[] services);
        void BlockController<T>() where T : Controller;
    }
}