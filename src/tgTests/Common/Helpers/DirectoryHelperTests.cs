using System;
using System.IO;
using NUnit.Framework;
using TreeGecko.Library.Common.Helpers;

namespace tgTests.Common.Helpers
{
    [TestFixture]
    public class DirectoryHelperTests
    {
        [Test]
        public void BuildFolderIfMissing()
        {
            string path = Path.GetTempPath();
            string path2 = Path.Combine(path, Guid.NewGuid().ToString());

            DirectoryHelper.BuildFolderIfMissing(path2);
        }

        [Test]
        public void TestFolderPermissions()
        {
            string path = Path.GetTempPath();
            string path2 = Path.Combine(path, Guid.NewGuid().ToString());

            DirectoryHelper.BuildFolderIfMissing(path2);
            DirectoryHelper.TestFolderPermissions(path2);
        }

        [Test]
        public void TestFolderCopy()
        {
            string path = Path.GetTempPath();
            string path2 = Path.Combine(path, Guid.NewGuid().ToString());
            DirectoryHelper.BuildFolderIfMissing(path2);

            string path3 = Path.Combine(path, Guid.NewGuid().ToString());

            string filepath3 = Path.Combine(path2, "test.txt");
            File.WriteAllText(filepath3, "test this file");

            DirectoryHelper.FileCopy(path2, path3, true);
        }
    }
}
