using System;
using System.Collections.Generic;
using System.Text;

namespace DeWaste.DataFiles
{
    class DataProvider : IDataProvider
    {
        public Item get(string itemName)
        {
            Item toothbrush;
            toothbrush = new Item();
            toothbrush.name = "Toothbrush";
            toothbrush.description = "A toothbrush is a small brush used to clean the teeth, gums, and tongue. It is used to remove dental plaque and food from the teeth, and to stimulate the gums and keep them healthy.";
            toothbrush.imageSource = "https://i5.walmartimages.com/asr/f26ebfc2-aeb6-4c0d-aca8-82ce8f2b0023.2c5657d0aab07ee7eedf503b02ff12a2.png";
            return toothbrush;
        }

        
        public List<string> getSuggestions()
        {
            //fake suggestions
            List<string> suggestions;
            suggestions = new List<string>();
            suggestions.Add("toothbrush");
            suggestions.Add("sofa");
            suggestions.Add("chair");
            suggestions.Add("table");

            return suggestions;
        }
    }
}
