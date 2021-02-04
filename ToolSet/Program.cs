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
                    string arg = "";

                    arg = @"-p SASO.Attachments.Domain 1.0.0.1 -d C:\_abdelrahman\Dev\Maneh\ManehBackend\modules";
                    //arg = @"-n D:\Work\Common\PackageServer\Packages -d C:\TFS\CodeShell.Framework\DotNetCore";
                    //arg = @"-z D:\Work\ziptest D:\work\ziptest.zip";
                    //arg = @"-n ftp:administrator/DevServer123@ASGA_DEV:2121::P::/AsgaPackages/Packages -d C:\ASGA_TFS\Libraries\CodeShellCore\master";
                    //arg = @"-c ftp:abarkouky/Gabr1el2018@SRV-GENIAL:31::P::/SQL Backups/TopSide.bak D:/Download";
                    //arg = @"-c D:/Download/TopSide.bak ftp:abarkouky/Gabr1el2018@SRV-GENIAL:31::P::/SQL Backups";
                    args = arg.Split(' ');

                    // args = new[] { @"-c", @"C:\ASGA_TFS\FMS\Source_Code\master\FMS\SQL\Backups\FMS.ASGA_v2.5.bak", @"ftp:administrator/AsgaTechKsa963258741@i-maher.com:21::A::/FMS/SQL Backups/ASGA/" };
                    //args = new[] { $"-r", "User Id=app;Server=.;Password=123456;", "Configurator.Config_2", "C:\\ASGA_TFS\\Libraries\\Moldster\\master\\Configurator.Config.Api\\Backups\\Configurator.Config.bak" };
                    //args = new[] { $"-n", @"D:\Work\Common\PackageServer\Packages", "-d", @"C:\ASGA_TFS\Libraries\CodeShellCore\v2.11.9" };
                    //args = new[] { "-h" };
                    //args = new[] { "-q", "Server=.;User Id=app;Password=123456;Database=FMS.Configuration_v2.6", "update Resources set Name=Name" };
                    Dispatcher.Dispatch(args);
                    Console.ReadLine();
                }
                else
                {
                    Dispatcher.Dispatch(args);
                }


            }
            catch (NotEnoughArgumentsException)
            {
                Console.WriteLine("Not enough arguments provided");
                Dispatcher.Help(new Dispatcher());
            }
            catch (NonExistantOption ex)
            {
                Console.WriteLine(ex.Message);
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
