using CodeShellCore.Security.Authorization;
using System.Collections.Generic;

namespace FMS.Security
{
    public class FMSPermission : Permission
    {
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Details { get; set; }

        public FMSPermission(bool insert, bool update, bool delete, bool details)
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
                    FMSPermission cast = (FMSPermission)per;
                    cast.Insert = cast.Insert || item.CanInsert;
                    cast.Update = cast.Update || item.CanUpdate;
                    cast.Delete = cast.Delete || item.CanDelete;
                    cast.Details = cast.Details || item.CanViewDetails;
                }
                else
                {
                    p[item.Id] = new FMSPermission(item.CanInsert, item.CanUpdate, item.CanDelete, item.CanViewDetails);
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
                    p[item.Id] = new FMSPermission(false, false, false, false);
                }
            }

            return p;
        }
    }
}
