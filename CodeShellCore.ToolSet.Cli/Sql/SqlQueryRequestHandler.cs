using CodeShellCore.Cli;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Data.Sql;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Sql
{
    public class SqlQueryRequestHandler : CliRequestHandler<SqlQueryRequest>
    {
        public SqlQueryRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<SqlQueryRequest> builder)
        {
            builder.FillProperty(e => e.ConnectionString, "connection-string", 'c', 1, isRequired: true);
            builder.FillProperty(e => e.SqlQuery, "query-string", 'q', 2, isRequired: true);
        }

        protected override Task<Result> HandleAsync(SqlQueryRequest request)
        {
            var sql = new ToolSetSqlService();
            sql.ConnectionParams = new DbConnectionParams
            {
                ConnectionString = request.ConnectionString
            };
            Console.WriteLine("Executing '" + request.SqlQuery + "'...");
            var res = sql.RunSql(request.SqlQuery, null, true);
            if (res.IsSuccess)
            {
                Console.WriteLine("Success : " + res.Message);
            }
            else
            {
                using (var set = ColorSetter.Set(ConsoleColor.Red))
                {
                    Console.Write("Failed");
                    Console.WriteLine(res.ExceptionMessage);
                }

            }
            Console.WriteLine();
            return Task.FromResult(new Result());
        }
    }
}
