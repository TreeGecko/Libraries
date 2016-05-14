using System;
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

        [Test]
        public void TestCryptoStringNoKeyEncrypt()
        {
            string test = "Test String";

            CryptoString cs = new CryptoString();

            try
            {
                string crypto = cs.Encrypt(test);
                string plain = cs.Decrypt(crypto);

                Assert.Fail("Should have thrown an exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("savedKey and savedIV must be non-null.", ex.Message);
            }
        }

        [Test]
        public void TestCryptoStringNoKeyDecrypt()
        {
            string test = "Test String";

            CryptoString cs1 = new CryptoString(CryptoString.MakeKey(), CryptoString.MakeIV());
            string crypto = cs1.Encrypt(test);
            
            CryptoString cs2 = new CryptoString();

            try
            {
                string plain = cs2.Decrypt(crypto);

                Assert.Fail("Should have thrown an exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("savedKey and savedIV must be non-null.", ex.Message);
            }
        }

        [Test]
        public void TestCryptoStringDefaultConstructor()
        {
            string key = CryptoString.MakeKey();
            string iv = CryptoString.MakeIV();

            string test = "Test String";

            CryptoString cs = new CryptoString
            {
                Key = key,
                IV = iv
            };

            string crypto = cs.Encrypt(test);
            string plain = cs.Decrypt(crypto);

            Assert.AreEqual(test, plain);
        }

        [Test]
        public void TestCryptoGetters()
        {
            string key = CryptoString.MakeKey();
            string iv = CryptoString.MakeIV();

            CryptoString cs = new CryptoString
            {
                Key = key,
                IV = iv
            };

            string key2 = cs.Key;
            string iv2 = cs.IV;

            Assert.AreEqual(key, key2);
            Assert.AreEqual(iv, iv2);
        }


    }
}
