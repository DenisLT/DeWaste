using DeWaste.WebServices;
using NUnit.Framework;

namespace Testing
{
    public class Tests
    {
        IServiceProvider container;
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            DatabaseApi databaseApi = new DatabaseApi(container);
            Assert.Pass();
        }
    }
}