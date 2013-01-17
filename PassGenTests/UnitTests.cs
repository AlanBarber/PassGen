using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassGen;

namespace PassGenTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void ArgumentParserReturnsEmptyListWhenNoArgumentsProvided()
        {
            string[] args = new string[0];
            
            ArgumentParser argumentParser = new ArgumentParser();
            Dictionary<string, string> arguments = argumentParser.Parse(args);

            Assert.AreEqual(0, arguments.Count);
        }

        [TestMethod]
        public void ArgumentParserReturnsData()
        {
            string[] args = new string[1];
            args[0] = "-A";

            ArgumentParser argumentParser = new ArgumentParser();
            Dictionary<string, string> arguments = argumentParser.Parse(args);

            Assert.AreEqual(arguments["A"],"A");
        }

        [TestMethod]
        public void ArgumentParserReturnsArgumentWithoutDashAndValue()
        {
            string[] args = new string[6];
            args[0] = "-c";
            args[1] = "Mixed";
            args[2] = "-l";
            args[3] = "8";
            args[4] = "-p";
            args[5] = "Numeric";

            ArgumentParser argumentParser = new ArgumentParser();
            Dictionary<string, string> arguments = argumentParser.Parse(args);

            Assert.AreEqual(arguments["c"],"Mixed");
        }
    }
}
