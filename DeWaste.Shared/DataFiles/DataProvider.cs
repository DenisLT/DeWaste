using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.UI.Xaml;

namespace DeWaste.DataFiles
{
    class DataProvider : IDataProvider
    {
        List<String> suggestions;
        Dictionary<String, Item> items;
        string db_edit_timestamp = "0000000000";


        private string db_link = @"http://20.23.27.75:3000/items";

        HttpClient _httpClient;

        public DataProvider()
        {
#if __WASM__
            var innerHandler = new Uno.UI.Wasm.WasmHttpHandler();
#else
            var innerHandler = new HttpClientHandler();
#endif
            _httpClient = new HttpClient(innerHandler);

            items = new Dictionary<string, Item>();
            suggestions = new List<string>();
        }

        
       
        
        public bool canConnectToDB()
        {
            return true;
        }
        
        

        public Item get(string itemName)
        {
            if(!items.ContainsKey(itemName))
            {
                Item item = new Item();
                item.name = "Example Item";
                item.description = new List<String>();
                item.description.Add("Example description");
                item.imageSource = "/Assets/Images/logo.png";
                return item;
            }
            return items[itemName];
        }
        
        private async Task<List<string>> getSuggestionsFromDBAsync()
        {
            List<string> suggestions = new List<string>();
            suggestions.Add("yo");
            try
            {
                var response = await _httpClient.GetAsync(db_link + "?select=name");
                suggestions.Add("worked");
                Console.Write("worked");
            }
            catch(Exception e)
            {
                suggestions.Add(e.Message);
            }       
            
            return suggestions;
        }
        
        
        public async Task<List<string>> getSuggestionsAsync()
        {
            if(suggestions == null)
            {
                suggestions = await getSuggestionsFromDBAsync();
            }
            return suggestions;
        }

        public bool itemExists(string itemName)
        {
            return suggestions.Contains(itemName);
        }
    }
}
