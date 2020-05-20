using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Configurator.Dtos
{
    public class MovePageRequest
    {
        public string FromPath { get; set; }
        public string ToPath { get; set; }
        public long PageId { get; set; }
        public long DomainId { get; set; }
        public string TenantCode { get; set; }
    }
}
