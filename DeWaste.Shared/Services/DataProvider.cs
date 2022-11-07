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

namespace DeWaste.Services
{
    public class DataProvider
    {
        DatabaseApi databaseApi = new DatabaseApi();

        private ObservableCollection<Suggestion> suggestions = new ObservableCollection<Suggestion>();

        private Dictionary<int, Item> items = new Dictionary<int, Item>();


        private string suggestionsPath = "suggestions.json";
        private string itemsPath = "items.json";
        

        private async void SaveSuggestions()
        {
            string data = JsonSerializer.Serialize<ObservableCollection<Suggestion>>(suggestions);
            await FileHandler.WriteDataToFileAsync(suggestionsPath, data);
        }
        
        private async void LoadSavedSuggestions()
        {
            string data = await FileHandler.ReadFileContentsAsync(suggestionsPath);

            if (!data.IsNullOrEmpty())
            {
               
               suggestions = JsonSerializer.Deserialize<ObservableCollection<Suggestion>>(data);
            }

        }

        private async void LoadSavedItems()
        {
            string data = await FileHandler.ReadFileContentsAsync(itemsPath);

            if (!data.IsNullOrEmpty())
            {
                items = JsonSerializer.Deserialize<Dictionary<int, Item>>(data);
            }
        }

        private async void SaveItems()
        {
            string data = JsonSerializer.Serialize<Dictionary<int, Item>>(items);
            await FileHandler.WriteDataToFileAsync(itemsPath, data);
        }


        public DataProvider()
        {
            LoadSavedSuggestions();
            LoadSavedItems();
        }
        

        public async Task<ObservableCollection<Suggestion>> GetSimilar(string name)
        {
            
            ObservableCollection<Suggestion> res = await databaseApi.GetSuggestions();
            suggestions = new ObservableCollection<Suggestion>(suggestions.Union(res, new UniqueSuggestionIDComparer()));

            Regex similarStrings = new Regex(name, RegexOptions.IgnoreCase);
            var filteredRes = new ObservableCollection<Suggestion>(suggestions.Where(sug => similarStrings.IsMatch(sug.name)));

            SaveSuggestions();
            

            return filteredRes;
        }

        public async Task<Item> GetItemById(int id)
        {
           
            Item item = await databaseApi.GetItem(id: id);

            if (item != null)
            {
                if (item.categories == null)
                    item.categories = await databaseApi.GetCategories(id: id);
                items[id] = item;
            }
        
            SaveItems();
            return items[id];
        }
    }
}
