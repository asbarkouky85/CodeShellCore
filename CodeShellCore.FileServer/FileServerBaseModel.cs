using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public class FileServerBaseModel : IModel<long>, IChangeColumns
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
