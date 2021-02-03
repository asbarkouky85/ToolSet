using System;
using System.Net;

namespace ToolSet.Ftp
{
    public class FtpResult
    {
        public string ExceptionMessage { get; private set; }
        public bool IsSuccess => ((int)StatusCode) >= 200 && ((int)StatusCode) < 300;
        public byte[] Bytes { get; set; } = new byte[0];
        public FtpStatusCode StatusCode { get; set; }
        public int Code { get; set; }
        public string[] Stack { get; set; }
        internal void SetException(Exception ex)
        {
            ExceptionMessage = ex.Message;
            Stack = ex.StackTrace?.Split('\n');
        }
    }
}
