using DeWaste;
using DeWaste.Models.DataModels;
using DeWaste.Services;

namespace Testing
{
    public class Tests
    {
        IServiceProvider container;
        IDataHandler dataProvider;
        [SetUp]
        public void Setup()
        {

            container = App.ConfigureDependencyInjection();
            dataProvider = (IDataHandler)container.GetService(typeof(IDataHandler));
        }

        [Test]
        public void Test1()
        {
            Item item = dataProvider.GetItemById(1).Result;
            Assert.That(item.id == 1);
        }
    }
}