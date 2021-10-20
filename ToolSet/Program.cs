using System;
using System.Diagnostics;

namespace ToolSet
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                if (Debugger.IsAttached)
                {
                    var testing = FunctionTypes.Copy;

                    switch (testing)
                    {
                        case FunctionTypes.Project:
                            args = new[] { "set-version", "-p", "SASO.Attachments.Domain", "-v", "1.0.0.2", "-d", @"C:\_abdelrahman\Dev\Maneh\ManehBackend\modules" };
                            break;
                        case FunctionTypes.Nuget:
                            args = new[] { "upload-nuget", "-s", @"C:\_abdelrahman\Personal\_gitHub\CodeShellCore", "-t", @"C:\_abdelrahman\Personal\Nuget" };
                            args = new[] { "upload-nuget", "-s", @"C:\_abdelrahman\Personal\_gitHub\CodeShellCore", "-t", @"ftp:genial\ftp_user/Genial963258741@196.202.126.106:21::P::/NugetServer/Packages" };
                            break;
                        case FunctionTypes.Zip:
                            args = new[] { "zip", "-s", @"C:\_abdelrahman\Work\ziptest", "-t", @"C:\_abdelrahman\Work\ziptest.zip" };
                            break;
                        case FunctionTypes.Copy:
                            args = new[] { "copy", "-s", @"C:\_abdelrahman\Work\ziptest\Info.txt", "-t", @"ftp:genial\ftp_user/Genial963258741@196.202.126.106:21::P::/" };
                            break;
                        case FunctionTypes.SqlRestore:
                            args = new[] { $"sql-restore", "-c", "User Id=app;Server=.;Password=123456;", "-d", "Configurator.Config_2", "-b", "C:\\ASGA_TFS\\Libraries\\Moldster\\master\\Configurator.Config.Api\\Backups\\Configurator.Config.bak" };
                            break;
                        case FunctionTypes.SqlQuery:
                            args = new[] { "sql-exec", "-c", "Server=.;User Id=app;Password=123456;Database=FMS.Configuration_v2.6", "-q", "update Resources set Name=Name" };
                            break;
                        case FunctionTypes.AbpJson:
                            args = new[] { @"sync-loc-abp", "-p", @"modules\Maneh.IEC\src\Maneh.IEC.Domain.Shared\Localization\IEC", "-d", @"C:\_abdelrahman\Dev\Maneh\ManehBackend" };
                            break;
                    }
                }

                var sh = new ToolSetShell(args);
                var t = sh.DispatchAsync();
                t.Wait();
                if (Debugger.IsAttached)
                    Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
