using DeWaste;
using DeWaste.Models.DataModels;
using DeWaste.Services;

namespace Testing
{
    public class Tests
    {
        IServiceProvider container;
        IDataProvider dataProvider;
        [SetUp]
        public void Setup()
        {

            container = App.ConfigureDependencyInjection();
            dataProvider = (IDataProvider)container.GetService(typeof(IDataProvider));
        }

        [Test]
        public void Test1()
        {
            Item item = dataProvider.GetItemById(1).Result;
            Assert.That(item.id == 1);
        }
    }
}