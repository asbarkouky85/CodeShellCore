using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public class FileServerBaseModel : IEntity<long>, IChangeColumns
    {
        public FileServerBaseModel() 
        {
            Id = Utils.GenerateID();
        }
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
