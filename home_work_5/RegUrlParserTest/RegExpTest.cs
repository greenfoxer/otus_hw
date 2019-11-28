using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using home_work_5;
using System.IO;

namespace RegUrlParserTest
{
    [TestFixture]
    public class RegExpTest
    {
        [Test]
        public void CorrectInputTest()
        {
            string testData = "qwerasdfzxcv";
            string expected = "Address is not valid!";
            string[] param = { testData };

            var currentConsoleOutput = Console.Out;
            


        }
    }
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
