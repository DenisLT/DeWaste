using System;
using System.Collections.Generic;
using System.Text;

namespace DeWaste.DataFiles
{
    interface IDataProvider
    {
        public List<string> getSuggestions();
        
        public Item get(string itemName);
    }
}

class Item
{
    public string name;
    public string description;
    public string imageSource;
}