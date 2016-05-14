using NUnit.Framework;
using TreeGecko.Library.Common.Security;

namespace tgTests.Common.Security
{
    [TestFixture]
    public class CryptoStringTests
    {
        [Test]
        public void TestCryptoString()
        {
            string key = CryptoString.MakeKey();
            string iv = CryptoString.MakeIV();

            string test = "Test String";

            CryptoString cs = new CryptoString(key, iv);

            string crypto = cs.Encrypt(test);
            string plain = cs.Decrypt(crypto);

            Assert.AreEqual(test, plain);
        }

    }
}
