using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeWasteApi.Models
{
    public partial class Item
    {
        public long id { get; set; }

        public string name { get; set; }


        public string img { get; set; } = null;

        
        public string description { get; set; }
    }
}
