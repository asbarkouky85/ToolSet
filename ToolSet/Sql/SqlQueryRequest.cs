using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet.Sql
{
    public class SqlQueryRequest : RequestBase
    {
        public string ConnectionString { get; set; }
        public string SqlQuery { get; set; }
        public SqlQueryRequest(string[] args) : base(args)
        {
        }

        public override void Execute(Dispatcher service)
        {
            var sql = new SqlService();
            sql.ConnectionParams = new DbConnectionParams
            {
                ConnectionString = ConnectionString
            };
            Console.WriteLine("Executing '" + SqlQuery + "'...");
            var res = sql.RunSql(SqlQuery, null, true);
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

        }

        public override string[] GetHelp()
        {
            return new string[] {
                "[connection string] [sql query]",
                "Runs query using connection string"
            };
        }

        public override void ProcessArgs()
        {
            if (Arguments.Length < 3)
            {
                throw new NotEnoughArgumentsException();
            }
            ConnectionString = Arguments[1];
            SqlQuery = Arguments[2];
        }
    }
}
