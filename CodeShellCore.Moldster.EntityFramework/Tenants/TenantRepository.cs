using CodeShellCore.Cli;
using CodeShellCore.Moldster.Sql;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantRepository : MoldsterRepository<Tenant, MoldsterContext>, ITenantRepository
    {
        readonly IOutputWriter _out;
        public TenantRepository(MoldsterContext con, IOutputWriter output) : base(con)
        {
            _out = output;
        }

        public async Task<SyncResult> SyncTenants(long src, long tar)
        {
            var con = DbContext;

            var d = await con.SyncTenants(src, tar);
            if (d != null)
            {
                _out.WriteLine();
                using (_out.Set(ConsoleColor.DarkCyan))
                {
                    _out.WriteLine("Synced tenant '" + d.SourceTenant + "' to '" + d.TargetTenant + "'");
                }
                _out.WriteLine("------------------------------------");
                _out.WriteLine();

                _out.Write("Added Pages : ");

                _out.GotoColumn(5);
                _out.WriteLine(d.AddedPages.ToString());

                _out.Write("Added Controls : ");
                _out.GotoColumn(5);
                _out.WriteLine(d.AddedPageControls.ToString());

                _out.Write("Updated Pages : ");
                _out.GotoColumn(5);
                _out.WriteLine(d.UpdatedPages.ToString());

                _out.Write("Updated Controls : ");
                _out.GotoColumn(5);
                _out.WriteLine(d.UpdatedPageControls.ToString());

                _out.Write("Added Navigation Pages : ");
                _out.GotoColumn(5);
                _out.WriteLine(d.NavigationPages.ToString());

                _out.WriteLine();

            }
            _out.Write("Updating viewparams");

            return d;

        }
    }
}
