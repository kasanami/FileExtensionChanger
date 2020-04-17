using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileExtensionChanger.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ChangeExtensionTest()
        {
            Assert.AreEqual(@"Hoge.jpg", Program.ChangeExtension(@"Hoge.txt", FileType.Jpeg));
            Assert.AreEqual(@"Piyo\Hoge.jpg", Program.ChangeExtension(@"Piyo\Hoge.txt", FileType.Jpeg));
            Assert.AreEqual(@"C:\Piyo\Hoge.jpg", Program.ChangeExtension(@"C:\Piyo\Hoge.txt", FileType.Jpeg));
        }
    }
}