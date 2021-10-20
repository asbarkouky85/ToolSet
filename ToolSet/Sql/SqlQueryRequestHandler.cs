using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet.Sql
{
    public class SqlQueryRequestHandler : CliRequestHandler<SqlQueryRequest>
    {
        public SqlQueryRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<SqlQueryRequest> builder)
        {
            builder.FillProperty(e => e.ConnectionString, 'c', "connection-string", true);
            builder.FillProperty(e => e.SqlQuery, 'q', "query-string", true);
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
