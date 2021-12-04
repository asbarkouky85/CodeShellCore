using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Services
{
    public interface ISpaFallbackHandler
    {
        string DefaultTenant { get; }
        Task HandleRequestAsync(HttpContext con);
    }
}
