// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using ToolSet.Versions;

namespace ToolSet.UnitTests
{
    [TestFixture]
    public class ProjectFileTest
    {

        Mock<IFileReader> getReaderMock(string fileContents)
        {
            Mock<IFileReader> mock = new Mock<IFileReader>();
            mock.Setup(d => d.FileExists(It.IsAny<string>())).Returns(true);
            mock.Setup(d => d.GetFileName(It.IsAny<string>())).Returns("TestProject.csproject");
            mock.Setup(d => d.GetFolderFullName(It.IsAny<string>())).Returns("./");
            mock.Setup(d => d.GetAllLines(It.IsAny<string>())).Returns(fileContents.Split('\n').ToList());
            return mock;
        }

        [Test]
        public void PatternTest()
        {
            string st = "ftp:administrator/DevServer123@Asga_dev:2121::";
            string patt = "ftp:(.*)/(.*)@(.*)::(.*)";
            if (st.GetPatternContents(patt, out string[] res))
            {
                Assert.That(res.Length >= 4,"returned should be at least 4");
                if (res.Length >= 4)
                {
                    Assert.AreEqual("administrator", res[0]);
                    Assert.AreEqual("DevServer123", res[1]);
                    Assert.AreEqual("Asga_dev:2121", res[2]);
                    Assert.AreEqual("", res[3]);
                }
                
                
            }
        }

        [Test]
        public void IsCore_TestBoth()
        {
            string project = @"

<TargetFramework>core</TargetFramework>
";
            var moq = getReaderMock(project);

            var proj = new ProjectFile("", moq.Object);
            Assert.AreEqual(true, proj.IsCore, "using " + project);

            moq = getReaderMock("");
            proj = new ProjectFile("", moq.Object);
            Assert.AreEqual(false, proj.IsCore, "using empty string");
        }

        [Test]
        public void SetVersion_Core()
        {
            string[] res = new string[0];
            string project =
@"<root>
    <TargetFramework>core</TargetFramework>
    <AssemblyVersion>1.0.0.1</AssemblyVersion>
</root>";

            string[] expected = new string[]{
                "<root>",
                "<TargetFramework>core</TargetFramework>",
                "<Version>1.3.0.1</Version>",
                "<AssemblyVersion>1.3.0.1</AssemblyVersion>",
                "<FileVersion>1.3.0.1</FileVersion>",
                "</root>"
            };
            var moq = getReaderMock(project);

            moq.Setup(d => d.WriteAllLines(It.IsAny<string>(), It.IsAny<List<string>>()))
                .Callback<string, List<string>>((d, s) =>
                {
                    res = s.Select(e => e.Trim()).ToArray();
                });
            var proj = new ProjectFile("", moq.Object);
            var req = new ProjectVersionRequest(new string[0]);
            req.LongVersion = "1.3.0.1";
            req.ShortVersion = "1.3";
            proj.SetVersion(req);
            proj.Save();
            Assert.AreEqual(expected, res);
        }


        [Test]
        public void SetVersion_NotCore()
        {
            string[] res = new string[0];
            string project =
@"";

            string[] expected = new string[]{
                "",
                "[assembly: AssemblyTitle(\"TestProjectect-v1.3\")]",                "[assembly: AssemblyProduct(\"TestProjectect-v1.3\")]",                "[assembly: AssemblyVersion(\"1.3.0.1\")]",                "[assembly: AssemblyFileVersion(\"1.3.0.1\")]",
            };
            var moq = getReaderMock(project);

            moq.Setup(d => d.WriteAllLines(It.IsAny<string>(), It.IsAny<List<string>>()))
                .Callback<string, List<string>>((d, s) =>
                {
                    res = s.Select(e => e.Trim()).ToArray();
                });
            var proj = new ProjectFile("", moq.Object);
            var req = new ProjectVersionRequest(new string[0]);
            req.LongVersion = "1.3.0.1";
            req.ShortVersion = "1.3";
            proj.SetVersion(req);
            proj.Save();
            Assert.AreEqual(expected, res);
        }
    }
}
