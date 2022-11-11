using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeWaste.Models.DataModels
{
    public partial class Category
    {
        [JsonPropertyName("category_id")]
        public int category_id { get; set; }
    }
}
