using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class MoldsterRepository<T, TContext> : Repository_Int64<T, TContext>
        where T : class, IMoldsterModel
        where TContext : DbContext
    {
        public MoldsterRepository(TContext con) : base(con)
        {
        }

        public override void Add(T obj)
        {
            if (obj.Id == 0)
                obj.Id = Utils.GenerateID();
            base.Add(obj);
        }
    }
}
