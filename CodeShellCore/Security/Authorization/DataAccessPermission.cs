using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class DataAccessPermission
    {
        public string CollectionId { get; set; }
        public IEnumerable<string> Actions { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Details { get; set; }

        public DataAccessPermission(bool insert, bool update, bool delete, bool details)
        {
            Insert = insert;
            Update = update;
            Delete = delete;
            Details = details;
        }

        public DataAccessPermission() { }


    }
}
