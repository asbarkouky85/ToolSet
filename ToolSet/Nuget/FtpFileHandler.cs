using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSet.Ftp;

namespace ToolSet.Nuget
{
    public class FtpFileHandler : IFileHandler
    {
        string folder;
        FTPClient cl;
        public FtpFileHandler(string path, bool isFile = false)
        {
            cl = FTPClient.FromString(path, out folder);
            
        }

        public byte[] GetFile()
        {
            var f = cl.DownloadFile(folder);
            if (f.IsSuccess)
            {
                return f.Bytes;
            }
            else
            {
                Console.WriteLine(f.ExceptionMessage);
                return new byte[0];
            }
        }

        public string GetFileName()
        {
            return folder.GetAfterLast("/");
        }

        public bool SaveFile(string fileName, byte[] file, out string message)
        {
            var s = cl.UploadFile(file, Utils.CombineUrl(folder, fileName));
            message = s.ExceptionMessage;
            return s.IsSuccess;
        }

        public bool UploadPackage(string projectName, string version, string packagePath)
        {
            string path = Utils.CombineUrl(folder, projectName);
            var dir = cl.GetDirectoryList(path);
            var exists = dir.Contains(version);
            if (!exists)
            {
                byte[] byts = File.ReadAllBytes(packagePath);
                string dist = Utils.CombineUrl(folder, Path.GetFileName(packagePath));
                cl.UploadFile(byts, dist);
            }
            return !exists;
        }
    }
}
