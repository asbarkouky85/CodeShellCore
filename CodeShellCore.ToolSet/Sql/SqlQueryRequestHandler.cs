using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Data.Sql;
using CodeShellCore.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Sql
{
    public class SqlQueryRequestHandler : CliRequestHandler<SqlQueryRequest>
    {
        public SqlQueryRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Executes sql using connection string";

        protected override void Build(ICliRequestBuilder<SqlQueryRequest> builder)
        {
            builder.Property(e => e.ConnectionString, "connection-string", "c", 1, isRequired: true);
            builder.Property(e => e.SqlQuery, "query-string", "q", 2, isRequired: true);
        }

        protected override Task<Result> HandleAsync(SqlQueryRequest request, CancellationToken token)
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
