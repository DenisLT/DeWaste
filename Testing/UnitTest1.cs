using DeWaste;
using DeWaste.Models.DataModels;
using DeWaste.Services;

namespace Testing
{
    public class Tests
    {
        DataProvider dataProvider;
        [SetUp]
        public void Setup()
        {

            IServiceProvider container = App.ConfigureDependencyInjection();
            dataProvider = new DataProvider(container);
        }

        [Test]
        public void Test1()
        {
            Item item = dataProvider.GetItemById(1).Result;

            Assert.That(item.id == 1);
        }
    }
}