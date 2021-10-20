using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Threading.Tasks;

namespace ToolSet.Sql
{
    public class SqlRestoreRequestHandler : CliRequestHandler<SqlRestoreRequest>
    {
        public SqlRestoreRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<SqlRestoreRequest> builder)
        {
            builder.FillProperty(e => e.ConnectionString, 'c', "connection-string", true);
            builder.FillProperty(e => e.BackupPath, 'b', "backup-path", true);
            builder.FillProperty(e => e.DbName, 'd', "database", true);
        }

        protected override Task<Result> HandleAsync(SqlRestoreRequest request)
        {
            var sql = new ToolSetSqlService();
            sql.ConnectionParams = new DbConnectionParams
            {
                ConnectionString = request.ConnectionString
            };
            sql.RestoreDatabase(request.DbName, request.BackupPath);
            return Task.FromResult(new Result());
        }

        public string[] GetHelp()
        {
            return new string[]
            {
                "[connection string] [database name] [.bak file path]",
                "Restores database from backup file using the SQL server connection string"
            };
        }
    }
}
