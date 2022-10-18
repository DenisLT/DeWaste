using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using DeWaste.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using DeWaste.Services;
using DeWaste.Models.DataModels;

namespace DeWaste.Views
{
    public sealed partial class SearchView : Page
    {
        List<String> suggestions;
        Frame mainContent;
        public SearchViewModel ViewModel;
        IServiceProvider container = ((App)App.Current).Container;
        DataProvider dataprovider;

        public SearchView()
        {
            this.InitializeComponent();
            ViewModel = new SearchViewModel();
            dataprovider = (DataProvider)container.GetService(typeof(DataProvider));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainContent = (Frame)e.Parameter;
        }

        
        private async void ItemChosen(object sender, SelectionChangedEventArgs args)
        {
            Suggestion suggestion = (Suggestion)args.AddedItems[0];
            Item item = await dataprovider.GetItemById((int)suggestion.id);
            mainContent.Navigate(typeof(ItemView), item);
        }
    }
}
