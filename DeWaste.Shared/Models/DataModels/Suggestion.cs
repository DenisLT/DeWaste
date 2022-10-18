using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeWaste.Models.DataModels
{
    public partial class Suggestion
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("id")]
        public int id { get; set; }
    }
}
