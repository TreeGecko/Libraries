using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TreeGecko.Library.Common.Security;

namespace tgTests.Common.Security
{
    [TestFixture]
    public class MD5HashTests
    {
        [Test]
        public void TestMD5File()
        {
            string tempFolder = Path.GetTempPath();
            Guid temp = Guid.NewGuid();

            string file = Path.Combine(tempFolder, temp + ".txt");

            string text = "This is a test of the emergency broadcast system.";
            File.WriteAllText(file, text);

            string hash = MD5Hash.GetMD5HashFromFile(file);
            Assert.IsNotNull(hash);
        }

        [Test]
        public void TestMD5String()
        {
            string temp = "This is a test of the emergency broadcast system.";
            string hash = MD5Hash.GetMD5HashFromString(temp);
            Assert.IsNotNull(hash);
        }
    }
}
