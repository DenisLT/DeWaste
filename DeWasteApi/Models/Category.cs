using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeWasteApi.Models
{
    public partial class Category
    {
        
        public int id { get; set; }

        public string name;

        public string description { get; set; }

    }
}
