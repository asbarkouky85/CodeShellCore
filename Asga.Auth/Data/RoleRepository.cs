using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CodeShellCore.Security;
using Asga.Common.Services;
using Asga.Data;


namespace Asga.Auth.Data
{
    public class RoleRepository : AsgaRepository<Role,AuthContext>,IRoleRepository,IAsgaRoleRepository
    {
        public RoleRepository(AuthContext con , AsgaCollectionService ser) : base(con,ser) { }

        public override LoadResult<Role> Find(ListOptions<Role> opts)
        {
            return Loader.Where(d => !d.IsUserRole).LoadWith(opts);
        }

        public override LoadResult<TR> FindAs<TR>(Expression<Func<Role, TR>> exp, ListOptions<TR> opts, Expression<Func<Role, bool>> cond = null)
        {
            var q = Loader.Where(d => !d.IsUserRole);
            if (cond != null)
                q = q.Where(cond);

            return q.Select(exp).LoadWith(opts);
        }

        public DateTime GetLastUpdateDate()
        {
            var x = DbContext.Roles.Max(i => i.CreatedOn)?? DateTime.MinValue;
            var x2 = DbContext.Roles.Max(i => i.UpdatedOn) ?? DateTime.MinValue;
            return x > x2 ? x : x2;
        }

        public IEnumerable<string> GetUserRoles(object userId)
        {
            return DbContext.UserRoles.Where(d => d.UserId.Equals(userId)).Select(d => d.RoleId.ToString()).ToList();
        }
    }
}
