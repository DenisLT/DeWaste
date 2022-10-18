using DeWaste.Models.DataModels;
using DeWaste.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;

namespace DeWaste.Services
{
    public class DataProvider
    {
        DatabaseApi databaseApi = new DatabaseApi();

        private ObservableCollection<Suggestion> suggestions = new ObservableCollection<Suggestion>();

        private Dictionary<int, Item> items = new Dictionary<int, Item>();


        public async Task<ObservableCollection<Suggestion>> GetSimilar(string name)
        {
            ObservableCollection<Suggestion> res = await databaseApi.GetSuggestions();
            suggestions = new ObservableCollection<Suggestion>(suggestions.Union(res, new UniqueSuggestionIDComparer()));

            Regex similarStrings = new Regex(name, RegexOptions.IgnoreCase);
            var filteredRes = new ObservableCollection<Suggestion>(suggestions.Where(sug => similarStrings.IsMatch(sug.name)));

            return filteredRes;
        }

        public async Task<Item> GetItemById(int id)
        {
            if (items.ContainsKey(id))
                return items[id];

            Item item = await databaseApi.GetItem(id: id);
            items.Add(id, item);
            return items[id];
        }
    }
}
