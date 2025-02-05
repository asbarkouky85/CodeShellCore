using CodeShellCore.Files.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CodeShellCore.UnitTest.Other
{
    public class LoggingTests : UnitTestClass
    {
        [SetUp]
        public void SetUp()
        {
            Shell.Start(new UnitTestShell(d =>
            {
            }));
        }

        [Test]
        public void WriteLog()
        {
            Logger.WriteLine("log log");
            FileAssert.Exists(Logger.Default.Location.FilePath);
            
        }
    }
}
