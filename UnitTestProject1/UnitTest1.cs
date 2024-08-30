using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBD.Domain.Interface;
using SBD.Infrastructure.Service;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ISBDService sbdService = new SBDService(new ScaneService(), new PrintService(), new FileService());
            var temp = sbdService.GetLuggageWieght();
            Assert.IsNotNull(temp);
        }
    }
}
