using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeWaste.Models.DataModels;

namespace DeWaste.WebServices
{
    public class ItemGetApi : WebApiBase
    {
        public async Task<ObservableCollection<Item>> Search(string name)
        {
            var result = await this.GetAsync("http://20.23.27.75:3000/items?lower_name=like.*" + name.ToLower() + "*");

            if (result != null)
            {
                return JsonSerializer.Deserialize<ObservableCollection<Item>>(result);
            }


            return new ObservableCollection<Item>();
        }
    }
}
