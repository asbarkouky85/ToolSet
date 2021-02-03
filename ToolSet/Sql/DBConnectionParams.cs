using System;
using System.Collections.Generic;
using System.Text;

namespace ToolSet.Sql
{
    public class DbConnectionParams
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        private string _connection;
        public string ConnectionString
        {
            get
            {
                if (_connection != null)
                    return _connection;
                return $"Server={Server};User Id={UserId};Password={Password};" + (Database == null ? "" : $"Database={Database}");
            }
            set
            {
                _connection = value;
            }
        }
    }
}
