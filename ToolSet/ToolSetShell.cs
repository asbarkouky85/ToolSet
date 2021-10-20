using CodeShellCore.Cli.Routing;
using ToolSet.Ftp;
using ToolSet.Localization;
using ToolSet.Nuget;
using ToolSet.Sql;
using ToolSet.Versions;
using ToolSet.Zip;

namespace ToolSet
{
    public class ToolSetShell : CliDispatchShell
    {
        public ToolSetShell(string[] args) : base(args)
        {
        }

        protected override void RegisterHandlers(ICliDispatcherBuilder builder)
        {
            builder.AddHandler<AbpSyncLanguagesRequestHandler>("sync-loc-abp");
            builder.AddHandler<NugetPublishRequestHandler>("upload-nuget");
            builder.AddHandler<SqlQueryRequestHandler>("sql-exec");
            builder.AddHandler<SqlRestoreRequestHandler>("sql-restore");
            builder.AddHandler<ProjectVersionRequestHandler>("set-version");
            builder.AddHandler<ZipRequestHandler>("zip");
            builder.AddHandler<CopyRequestHandler>("copy");

        }
    }
}
