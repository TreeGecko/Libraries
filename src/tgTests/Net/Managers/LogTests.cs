using System;
using NUnit.Framework;

namespace tgTests.Net.Managers
{
    [TestFixture]
    public class Logtests
    {
        [Test]
        public void LogException()
        {
            MockCoreManager manager = new MockCoreManager();

            Exception ex = new Exception("this is an exception");
            manager.LogException(Guid.NewGuid(), ex);
            manager.LogException(Guid.NewGuid(), null);
        }

        [Test]
        public void LogInfo()
        {
            MockCoreManager manager = new MockCoreManager();

            manager.LogInfo(Guid.NewGuid(), "this is info");
            manager.LogInfo(Guid.NewGuid(), null);
        }

        [Test]
        public void LogVerbose()
        {
            MockCoreManager manager = new MockCoreManager();

            manager.LogVerbose(Guid.NewGuid(), "this is verbose");
            manager.LogVerbose(Guid.NewGuid(), null);
        }

        [Test]
        public void LogWarning()
        {
            MockCoreManager manager = new MockCoreManager();

            manager.LogWarning(Guid.NewGuid(), "this is a warning");
            manager.LogWarning(Guid.NewGuid(), null);
        }

    }
}
