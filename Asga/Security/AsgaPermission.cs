using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using Asga.Auth.Views;

namespace Asga.Security
{
    public class AsgaPermission : Permission
    {
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Details { get; set; }

        public AsgaPermission(bool insert, bool update, bool delete, bool details)
        {
            Insert = insert;
            Update = update;
            Delete = delete;
            Details = details;
        }

        public static Dictionary<string, Permission> GetDictionary(IEnumerable<ResourceV> q,
            IEnumerable<ResourceActionV> q2)
        {
            Dictionary<string, Permission> p = new Dictionary<string, Permission>();

            foreach (var item in q)
            {
                Permission per;
                if (p.TryGetValue(item.Id, out per))
                {
                    AsgaPermission cast = (AsgaPermission)per;
                    cast.Insert = cast.Insert || item.CanInsert;
                    cast.Update = cast.Update || item.CanUpdate;
                    cast.Delete = cast.Delete || item.CanDelete;
                    cast.Details = cast.Details || item.CanViewDetails;
                }
                else
                {
                    p[item.Id] = new AsgaPermission(item.CanInsert, item.CanUpdate, item.CanDelete, item.CanViewDetails);
                }
            }

            foreach (var item in q2)
            {
                Permission per;
                if (p.TryGetValue(item.Id, out per))
                {
                    per.Append(item.Action);
                }
                else
                {
                    p[item.Id] = new AsgaPermission(false, false, false, false);
                }
            }

            return p;
        }
    }
}
