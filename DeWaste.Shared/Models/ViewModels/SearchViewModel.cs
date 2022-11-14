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
using DeWaste.Services;

namespace DeWaste.Models.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        private bool _isBusy;
        private string _searchTerm = "";
        private ObservableCollection<Suggestion> _searchResults = new ObservableCollection<Suggestion>();
        private DataProvider dataProvider;
        IServiceProvider container = ((App)App.Current).Container;




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

        public ObservableCollection<Suggestion> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }
        
        public SearchViewModel()
        {
            dataProvider = (DataProvider)container.GetService(typeof(DataProvider));
        }
        
        public async Task SearchItem()
        {
            if(!string.IsNullOrWhiteSpace(SearchTerm))
            {
                try
                {
                    SearchResults = await dataProvider.GetSimilar(name: SearchTerm);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
