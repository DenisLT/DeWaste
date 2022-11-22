using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeWaste.Logging;
using DeWaste.Models.DataModels;

namespace DeWaste.WebServices
{
    public class DatabaseApi : WebApiBase
    {
        string url = "http://20.23.27.75:3000/";

        public async Task<ObservableCollection<Item>> GetSimilar(string name)
        {
            try
            {
                var result = await this.GetAsync(url + "items?lower_name=like.*" + name.ToLower() + "*");

                if (result != null)
                {
                    return JsonSerializer.Deserialize<ObservableCollection<Item>>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            return new ObservableCollection<Item>();
        }
        
        public async Task<ObservableCollection<Suggestion>> GetSuggestions()
        {
            try
            {
                var result = await this.GetAsync(url + "items?select=name,id");

                if (result != null)
                {
                    return JsonSerializer.Deserialize<ObservableCollection<Suggestion>>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            return new ObservableCollection<Suggestion>();
        }

        public async Task<Item> GetItem(int id)
        {
            try
            {
                var result = await GetAsync(url + "items?id=eq." + id);

                if (result != null)
                {
                    return JsonSerializer.Deserialize<ObservableCollection<Item>>(result)[0];
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return null;
        }

        public async Task<List<Category>> GetCategories(int id)
        {
            try
            {
                var result = await GetAsync(url + "items_categories?item_id=eq." + id);

                if (result != null)
                {
                    return JsonSerializer.Deserialize<List<Category>>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return new List<Category>();
        }
    }
}
