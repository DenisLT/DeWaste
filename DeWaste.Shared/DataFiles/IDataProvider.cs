using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeWaste.DataFiles
{
    interface IDataProvider
    {
        public Task<List<string>> getSuggestionsAsync();

        public bool canConnectToDB();

        public bool itemExists(string itemName);
        
        public Item get(string itemName);
    }
}

class Item
{
    [JsonPropertyName("name")]
    public string name;
    [JsonPropertyName("desc")]
    public List<String> description;
    [JsonPropertyName("img")]
    public string imageSource;
    public string timestamp;
}