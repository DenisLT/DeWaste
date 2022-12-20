using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;

namespace DeWaste.Models.DataModels
{
    public partial class Item
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        
        [JsonPropertyName("img")]
        public string img { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("categories")]
        public ObservableCollection<Category> categories { get; set; }
    }
}
