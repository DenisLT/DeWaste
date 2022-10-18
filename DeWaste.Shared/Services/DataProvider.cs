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


        public async Task<ObservableCollection<Suggestion>> GetSimilar(string name)
        {
            ObservableCollection<Suggestion> res = await databaseApi.GetSuggestions();

            

            Regex similarStrings = new Regex(name, RegexOptions.IgnoreCase);

            var filteredRes = new ObservableCollection<Suggestion>(res.Where(sug => similarStrings.IsMatch(sug.name)));

            return filteredRes;
        }
    }
}
