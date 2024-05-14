using CodeShellCore.Proxy;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Proxy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    [Area("codeshell")]
    public class ApiDefinitionController : BaseApiController
    {
        readonly IProxyDocumentationService _service;

        public ApiDefinitionController(IProxyDocumentationService service)
        {
            _service = service;
        }

        public async Task<DocumentDto> Index()
        {
            return await _service.GetDocument();
        }
    }
}
