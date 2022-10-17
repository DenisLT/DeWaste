using DeWaste.Models.DataModels;
using DeWaste.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeWaste.Models.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        private bool _isBusy;
        private string _searchTerm = "";
        private ObservableCollection<Item> _searchResults = new ObservableCollection<Item>();
        private ItemGetApi _itemGetApi = new ItemGetApi();


        

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchItem();
            }
        }

        public ObservableCollection<Item> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }
        
        public SearchViewModel()
        {
            
        }
        
        public async Task SearchItem()
        {
            if(!string.IsNullOrWhiteSpace(SearchTerm))
            {
                try
                {
                    var result = await _itemGetApi.Search(SearchTerm);
                    
                    SearchResults = new ObservableCollection<Item>(result);


                    if (result.Any())
                    {
                        var res = new ObservableCollection<Item>(result);
                        SearchResults = res;
                    }

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
