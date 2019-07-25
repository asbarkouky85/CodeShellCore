using CodeShellCore.Moldster.Db.Razor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class CreateModalDTO
    {
        public long Id { get; set; }
        public string Domain { get; set; }
        public string TenantCode { get; set; }
        public string Name { get; set; }
        public string CategoryPath { get; set; }
        public long? CategoryId { get; set; }
        public ViewParams ViewParams { get; set; }
        public string Layout { get; set; }
    }
}
