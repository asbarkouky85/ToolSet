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
                    var testing = FunctionTypes.Nuget;

                    switch (testing)
                    {
                        case FunctionTypes.Project:
                            args = new[] { "set-version", "-p", "SASO.Attachments.Domain", "-v", "1.0.0.2", "-d", @"C:\_abdelrahman\Dev\Maneh\ManehBackend\modules" };
                            break;
                        case FunctionTypes.Nuget:
                            args = new[] { "upload-nuget","-s", @"C:\_abdelrahman\Personal\_gitHub\CodeShellCore", "-t", @"C:\_abdelrahman\Personal\Nuget" };
                            break;
                        case FunctionTypes.Zip:
                            args = new[] { "zip", "-s", @"C:\_abdelrahman\Work\ziptest", "-t", @"C:\_abdelrahman\Work\ziptest.zip" };
                            break;
                        case FunctionTypes.Copy:
                            break;
                        case FunctionTypes.SqlRestore:
                            break;
                        case FunctionTypes.SqlQuery:
                            break;
                        case FunctionTypes.SqlBackup:
                            break;
                        case FunctionTypes.AbpJson:
                            args = new[] { @"sync-loc-abp","-p", @"modules\Maneh.IEC\src\Maneh.IEC.Domain.Shared\Localization\IEC", "-d", @"C:\_abdelrahman\Dev\Maneh\ManehBackend" };
                            break;
                        default:
                            break;
                    }
                    //arg = @"-n D:\Work\Common\PackageServer\Packages -d C:\TFS\CodeShell.Framework\DotNetCore";
                    //arg = @"-z D:\Work\ziptest D:\work\ziptest.zip";
                    //arg = @"-n ftp:administrator/DevServer123@ASGA_DEV:2121::P::/AsgaPackages/Packages -d C:\ASGA_TFS\Libraries\CodeShellCore\master";
                    //arg = @"-c ftp:abarkouky/Gabr1el2018@SRV-GENIAL:31::P::/SQL Backups/TopSide.bak D:/Download";
                    //arg = @"-c D:/Download/TopSide.bak ftp:abarkouky/Gabr1el2018@SRV-GENIAL:31::P::/SQL Backups";

                    //args = arg.Split(' ');

                    // args = new[] { @"-c", @"C:\ASGA_TFS\FMS\Source_Code\master\FMS\SQL\Backups\FMS.ASGA_v2.5.bak", @"ftp:administrator/AsgaTechKsa963258741@i-maher.com:21::A::/FMS/SQL Backups/ASGA/" };
                    //args = new[] { $"-r", "User Id=app;Server=.;Password=123456;", "Configurator.Config_2", "C:\\ASGA_TFS\\Libraries\\Moldster\\master\\Configurator.Config.Api\\Backups\\Configurator.Config.bak" };
                    //args = new[] { $"-n", @"D:\Work\Common\PackageServer\Packages", "-d", @"C:\ASGA_TFS\Libraries\CodeShellCore\v2.11.9" };
                    //args = new[] { "-h" };
                    //args = new[] { "-q", "Server=.;User Id=app;Password=123456;Database=FMS.Configuration_v2.6", "update Resources set Name=Name" };
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
