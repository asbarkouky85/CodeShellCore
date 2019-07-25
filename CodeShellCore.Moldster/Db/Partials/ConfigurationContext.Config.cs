using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using CodeShellCore.Text;
using CodeShellCore.Moldster.Db.Dto;

namespace CodeShellCore.Moldster.Db
{
    public partial class ConfigurationContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(Shell.GetConfigAs<string>("ConnectionStrings:Moldster"));
        }

        public SyncResult SyncTenants(long source, long target)
        {
            SqlParameter par = new SqlParameter("res",System.Data.SqlDbType.VarChar,-1) { Direction = System.Data.ParameterDirection.Output };
            Database.ExecuteSqlCommand("exec dbo.SyncTenants @p0,@p1,@res OUTPUT", source, target, par);
            if (par.Value != null)
                return par.Value.ToString().FromJson<SyncResult>();
            return null;
        }
    }
}
