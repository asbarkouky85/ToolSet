using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet.Sql
{
    public class SqlRestoreRequest : RequestBase
    {
        public string DbName { get; set; }
        public string BackupPath { get; set; }
        public string ConnectionString { get; set; }
        public SqlRestoreRequest(string[] args) : base(args)
        {
        }

        public override void Execute(Dispatcher service)
        {
            var sql = new SqlService();
            sql.ConnectionParams = new DbConnectionParams
            {
                ConnectionString = ConnectionString
            };
            sql.RestoreDatabase(DbName, BackupPath);
        }

        public override void ProcessArgs()
        {
            if (Arguments.Length < 4)
            {
                throw new NotEnoughArgumentsException();
            }
            ConnectionString = Arguments[1];
            DbName = Arguments[2];
            BackupPath = Arguments[3];
        }

        public override string[] GetHelp()
        {
            return new string[] 
            {
                "[connection string] [database name] [.bak file path]",
                "Restores database from backup file using the SQL server connection string"
            };
        }
    }
}
