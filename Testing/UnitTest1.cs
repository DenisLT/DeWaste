using DeWaste.Models.DataModels;
using DeWaste.Services;
using DeWaste.WebServices;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using DeWaste.Logging;
using System.Collections.ObjectModel;
using DeWaste.Models.ViewModels;

namespace Testing
{
    public class Tests
    {
        IServiceProvider container;

        public static IServiceProvider ConfigureDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<IDataHandler>((a) => new DataHandler(a));
            serviceCollection.AddSingleton<ILogger, DummyLogger>((a) => new DummyLogger(a));

            serviceCollection.AddSingleton<NavigationViewModel>((a) => new NavigationViewModel(a));
            serviceCollection.AddSingleton<ItemViewModel>();
            serviceCollection.AddSingleton<SearchViewModel>((a) => new SearchViewModel(a));


            return serviceCollection.BuildServiceProvider();
        }

        DataHandler dataHandler;

        [SetUp]
        public void Setup()
        {
            container = ConfigureDependencyInjection();
            dataHandler = (DataHandler)container.GetService(typeof(IDataHandler));
        }


        [Test]
        public void DatabaseApiTest()
        {
            DatabaseApi databaseApi = new DatabaseApi(container);
            Item item = databaseApi.GetItem(1).Result;
            Assert.That(item.id == 1);
        }

        [Test]
        public void DataHandlerTest()
        {
            Item item = dataHandler.GetItemById(1).Result;
            Assert.That(item.id == 1);
        }

        [Test]
        public void DataHandlerGetSuggestionsTest()
        {
            ObservableCollection<Suggestion> items = dataHandler.GetSimilar("Plastic Bottle").Result;
            Assert.That(items[0].name == "Plastic Bottle");
        }

        [Test]
        public void DataHandlerGetComments()
        {
            ObservableCollection<Comment> comments = dataHandler.GetCommentsByItemID(1).Result;
            Assert.That(comments[0].item_id == 1);
        }

        [Test]
        public void TestComments()
        {
            int item_id = 1;
            string content = "Test";
            Comment newComment = dataHandler.SubmitComment(item_id, content).Result;
            Assert.That(newComment.item_id == item_id && newComment.content == content);

            //like comment
            Comment likedComment = dataHandler.LikeComment(newComment).Result;
            Assert.That(likedComment.likes == 1);

            //dislike comment
            Comment dislikedComment = dataHandler.DislikeComment(likedComment).Result;
            Assert.That(dislikedComment.dislikes == 1);

            //undislike comment
            Comment undislikedComment = dataHandler.DislikeComment(dislikedComment).Result;
            Assert.That(undislikedComment.dislikes == 0);

            //dislike again
            dislikedComment = dataHandler.DislikeComment(undislikedComment).Result;
            Assert.That(dislikedComment.dislikes == 1);

            //like again
            likedComment = dataHandler.LikeComment(dislikedComment).Result;
            Assert.That(likedComment.likes == 1);

            //unlike comment
            Comment unlikedComment = dataHandler.LikeComment(likedComment).Result;
            Assert.That(unlikedComment.likes == 0);

            //delete comment
            Comment deletedComment = dataHandler.DeleteComment(unlikedComment.id).Result;
            Assert.That(deletedComment.id == unlikedComment.id);

        }

        [Test]
        public async Task TestSearchViewModel()
        {
            SearchViewModel searchViewModel = (SearchViewModel)container.GetService(typeof(SearchViewModel));
            searchViewModel.SearchTerm ="Plastic Bottle";
            await searchViewModel.SearchItem();
            Assert.That(searchViewModel.SearchResults[0].name == "Plastic Bottle");
        }

        [Test]
        public async Task TestItemViewModel()
        {
            ItemViewModel itemViewModel = (ItemViewModel)container.GetService(typeof(ItemViewModel));
            Item item = await dataHandler.GetItemById(1);            
            await itemViewModel.SetItem(item);
            Assert.That(itemViewModel.ItemName == "Plastic Bottle");

            itemViewModel.CommentText = "Test";
            await itemViewModel.SubmitComment();
            Comment newComment = itemViewModel.Comments[0];

            Assert.That(newComment.item_id == 1 && newComment.content == "Test");

            //like comment
            await itemViewModel.LikeComment(newComment);
            Comment likedComment = itemViewModel.Comments.Where(x => x.id == newComment.id).FirstOrDefault();
            Assert.That(likedComment.likes == 1);

            //dislike comment
            await itemViewModel.DislikeComment(likedComment);
            Comment dislikedComment = itemViewModel.Comments.Where(x => x.id == likedComment.id).FirstOrDefault();
            Assert.That(dislikedComment.dislikes == 1);

            //delete comment
            await itemViewModel.DeleteComment(dislikedComment);
            Assert.That(itemViewModel.Comments.Contains(dislikedComment) == false);
        }

        [Test]
        public async Task TestNavigationViewModel()
        {
            NavigationViewModel navigationViewModel = (NavigationViewModel)container.GetService(typeof(NavigationViewModel));
            navigationViewModel.FailedConnectToServer = true;
            Assert.That(navigationViewModel.FailedConnectToServer == true);

            navigationViewModel.FailedConnectToServer = false;
            Assert.That(navigationViewModel.FailedConnectToServer == false);
        }
    }
}