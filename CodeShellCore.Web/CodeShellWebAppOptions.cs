using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web
{
    public class CodeShellWebAppOptions
    {
        public bool UseHealthCheck { get; set; } = true;
        public string UrlRoot { get; set; } = "~";
        public string PublicRelativePath { get; set; } = "wwwroot";
        public bool UseCors { get; set; } = false;
        public bool UseSwagger { get; set; } = false;
        public string DefaultCorsOrigins { get; set; } = "http://localhost,http://localhost:4200";
        public bool UseAuditLogs { get; set; } = true;
    }
}
