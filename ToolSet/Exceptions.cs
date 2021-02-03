using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet
{
    public class NotEnoughArgumentsException : Exception { }

    public class NonExistantOption : Exception
    {
        string message;
        public override string Message
        {
            get
            {
                return message;
            }
        }
        public NonExistantOption(string ch)
        {
            message = "Unknown Option '-"+ch+"'";
        }
    }
}
