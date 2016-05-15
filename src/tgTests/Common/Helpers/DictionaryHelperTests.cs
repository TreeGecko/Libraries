using System;
using System.Collections.Generic;
using NUnit.Framework;
using tgTests.Mongo.Daos;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Security;

namespace tgTests.Common.Helpers
{
    [TestFixture]
    public class DictionaryHelperTests
    {
        [Test]
        public void DictionaryTest()
        {
            List<MockObject> objects = new List<MockObject>();

            MockObject mo1 = new MockObject
            {
                MockObjectGuid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Description = RandomString.GetRandomString(20)
            };
            objects.Add(mo1);

            MockObject mo2 = new MockObject
            {
                MockObjectGuid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Description = RandomString.GetRandomString(20)
            };
            objects.Add(mo2);

            MockObject mo3 = new MockObject
            {
                MockObjectGuid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Description = RandomString.GetRandomString(20)
            };
            objects.Add(mo3);

            MockObject mo4 = new MockObject
            {
                MockObjectGuid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Description = RandomString.GetRandomString(20)
            };
            objects.Add(mo4);

            Dictionary<Guid, MockObject> dictionary = DictionaryHelper.GetDictionary(objects);
            Assert.IsNotNull(dictionary);
        }

        [Test]
        public void NamedObjectDictionary()
        {
            List<DictionaryMockObject> objects = new List<DictionaryMockObject>();

            DictionaryMockObject mo1 = new DictionaryMockObject
            {
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10)
            };
            objects.Add(mo1);

            DictionaryMockObject mo2 = new DictionaryMockObject
            {
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10)
            };
            objects.Add(mo2);

            DictionaryMockObject mo3 = new DictionaryMockObject
            {
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10)
            };
            objects.Add(mo3);

            DictionaryMockObject mo4 = new DictionaryMockObject
            {
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10)
            };
            objects.Add(mo4);

            Dictionary<string, DictionaryMockObject> dictionary = DictionaryHelper.GetNamedObjectDictionary(objects);
            Assert.IsNotNull(dictionary);
        }
    }
}
