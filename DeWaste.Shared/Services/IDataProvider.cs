using DeWaste.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DeWaste.Services
{
    public interface IDataProvider
    {
        Task<Item> GetItemById(int id);
        Task<ObservableCollection<Suggestion>> GetSimilar(string name);
    }
}
