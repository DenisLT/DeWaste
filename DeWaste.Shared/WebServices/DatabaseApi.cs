using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeWaste.Models.DataModels;

namespace DeWaste.WebServices
{
    public class DatabaseApi : WebApiBase
    {
        public async Task<ObservableCollection<Item>> GetSimilar(string name)
        {
            var result = await this.GetAsync("http://20.23.27.75:3000/items?lower_name=like.*" + name.ToLower() + "*");

            if (result != null)
            {
                return JsonSerializer.Deserialize<ObservableCollection<Item>>(result);
            }

            return new ObservableCollection<Item>();
        }
        
        public async Task<ObservableCollection<Suggestion>> GetSuggestions()
        {
            var result = await this.GetAsync("http://20.23.27.75:3000/items?select=name,id");
            
            if(result != null)
            {
                return JsonSerializer.Deserialize<ObservableCollection<Suggestion>>(result);
            }
            
            return new ObservableCollection<Suggestion>();
        }

        public async Task<Item> GetItem(int id)
        {
            var result = await GetAsync("http://20.23.27.75:3000/items?id=eq." + id);
            
            if(result != null)
            {
                return JsonSerializer.Deserialize<ObservableCollection<Item>>(result)[0];
            }

            return new Item();
        }
    }
}
