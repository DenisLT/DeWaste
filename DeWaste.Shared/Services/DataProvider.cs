using DeWaste.Models.DataModels;
using DeWaste.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using Windows.Storage.Pickers;
using System.IO;
using System.Text.Json;
using Windows.Storage;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using DeWaste.Logging;

namespace DeWaste.Services
{
    public class DataProvider : IDataProvider
    {
        DatabaseApi databaseApi = null;

        private ObservableCollection<Suggestion> suggestions = new ObservableCollection<Suggestion>();

        private Dictionary<int, Item> items = new Dictionary<int, Item>();


        private string suggestionsPath = "suggestions.json";
        private string itemsPath = "items.json";

        IServiceProvider container;
        IFileHandler fileHandler;
        ILogger logger;



        private async void SaveSuggestions()
        {
            try
            {
                string data = JsonSerializer.Serialize<ObservableCollection<Suggestion>>(suggestions);
                await fileHandler.WriteDataToFileAsync(suggestionsPath, data);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }
        
        private async void LoadSavedSuggestions()
        {
            try
            {
                string data = await fileHandler.ReadFileContentsAsync(suggestionsPath);
                if (!data.IsNullOrEmpty())
                {
                    suggestions = JsonSerializer.Deserialize<ObservableCollection<Suggestion>>(data);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }

        private async void LoadSavedItems()
        {
            try
            {
                string data = await fileHandler.ReadFileContentsAsync(itemsPath);

                if (!data.IsNullOrEmpty())
                {
                    items = JsonSerializer.Deserialize<Dictionary<int, Item>>(data);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }

        private async void SaveItems()
        {
            try
            {
                string data = JsonSerializer.Serialize<Dictionary<int, Item>>(items);
                await fileHandler.WriteDataToFileAsync(itemsPath, data);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }


        public DataProvider(IServiceProvider container)
        {
            this.container = container;
            databaseApi = new DatabaseApi(container);
            logger = container.GetService(typeof(ILogger)) as ILogger;
            fileHandler = (IFileHandler)container.GetService(typeof(IFileHandler));
            LoadSavedSuggestions();
            LoadSavedItems();
        }
        

        public async Task<ObservableCollection<Suggestion>> GetSimilar(string name)
        {
            try
            {
                ObservableCollection<Suggestion> res = await databaseApi.GetSuggestions();
                suggestions = new ObservableCollection<Suggestion>(suggestions.Union(res, new UniqueSuggestionIDComparer()));

                Regex similarStrings = new Regex(name, RegexOptions.IgnoreCase);
                var filteredRes = new ObservableCollection<Suggestion>(suggestions.Where(sug => similarStrings.IsMatch(sug.name)));

                SaveSuggestions();


                return filteredRes;
            }
 
            catch (Exception ex)
            {
                logger.Log(ex.Message);
                return new ObservableCollection<Suggestion>();
            }
        }

        public async Task<Item> GetItemById(int id)
        {
            try
            {
                Item item = await databaseApi.GetItem(id: id);

                if (item != null)
                {
                    if (item.categories == null)
                        item.categories = await databaseApi.GetCategories(id: id);
                    items[id] = item;
                    SaveItems();
                }

                return items[id];
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
                return null;
            }
        }
    }
}
