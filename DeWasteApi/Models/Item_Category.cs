using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeWasteApi.Models
{
    public partial class Item_Category
    {

        public int id { get; set; }

        public int item_id { get; set; }

        public int category_id { get; set; }

    }
}
