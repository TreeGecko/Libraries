using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using tgTests.Net.Managers;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;
using TreeGecko.Library.Net.Enums;
using TreeGecko.Library.Net.Objects;

namespace tgTests.Net.Objects
{
    [TestFixture]
    public class CannedEmailTests
    {
        MockCoreManager mcm = new MockCoreManager();

        public void SerializationTest()
        {
            CannedEmail email = new CannedEmail
            {
                Active = true,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };

            TGSerializedObject tgs = email.GetTGSerializedObject();
            CannedEmail email2 = new CannedEmail();
            email2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(email.Active, email2.Active);
            Assert.AreEqual(email.Body, email2.Body);
            Assert.AreEqual(email.BodyType, email2.BodyType);
            Assert.AreEqual(email.Description, email2.Description);
            Assert.AreEqual(email.Guid, email2.Guid);
            Assert.AreEqual(email.Name, email2.Name);
            Assert.AreEqual(email.Subject, email2.Subject);
            Assert.AreEqual(email.To, email2.To);
            Assert.AreEqual(email.From, email2.From);
            Assert.AreEqual(email.ReplyTo, email2.ReplyTo);
        }

        [Test]
        public void AddCannedEmail()
        {
            CannedEmail email = new CannedEmail
            {
                Active = true,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };
            mcm.Persist(email);
        }

        [Test]
        public void UpdateCannedEmail()
        {
            CannedEmail email = new CannedEmail
            {
                Active = true,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };
            mcm.Persist(email);

            CannedEmail email2 = mcm.GetCannedEmail(email.Guid);
            Assert.IsNotNull(email2);

            email2.Body = RandomString.GetRandomString(10);
            mcm.Persist(email2);

            CannedEmail email3 = mcm.GetCannedEmail(email.Guid);
            Assert.IsNotNull(email3);
            Assert.AreEqual(email2.Body, email3.Body);
        }

        [Test]
        public void GetCannedEmails()
        {
            CannedEmail email = new CannedEmail
            {
                Active = true,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };
            mcm.Persist(email);

            CannedEmail email2 = new CannedEmail
            {
                Active = false,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };
            mcm.Persist(email2);

            List<CannedEmail> emails = mcm.GetCannedEmails();

            bool has1 = false;
            bool has2 = false;

            foreach (var ce in emails)
            {
                if (ce.Guid == email.Guid)
                {
                    has1 = true;
                }

                if (ce.Guid == email2.Guid)
                {
                    has2 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsTrue(has2);
        }

        [Test]
        public void GetActiveCannedEmails()
        {
            CannedEmail email = new CannedEmail
            {
                Active = true,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };
            mcm.Persist(email);

            CannedEmail email2 = new CannedEmail
            {
                Active = false,
                Body = RandomString.GetRandomString(10),
                BodyType = EmailBodyType.Text,
                Description = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Name = RandomString.GetRandomString(10),
                Subject = RandomString.GetRandomString(10),
                To = "noreply@treegecko.com",
                From = "noreploy@treegecko.com",
                ReplyTo = "noreply@treegecko.com"
            };
            mcm.Persist(email2);

            List<CannedEmail> emails = mcm.GetCannedEmails();

            bool has1 = false;
            bool has2 = false;

            foreach (var ce in emails)
            {
                if (ce.Guid == email.Guid)
                {
                    has1 = true;
                }

                if (ce.Guid == email2.Guid)
                {
                    has2 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsFalse(has2);
        }
    }
}
