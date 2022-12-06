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
        SearchViewModel ViewModel;
        IServiceProvider container;
        IDataProvider dataprovider;

        public SearchView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationParameters parameters = (NavigationParameters)e.Parameter;
            container = parameters.container;
            mainContent = parameters.mainContent;
            ViewModel = container.GetService(typeof(SearchViewModel)) as SearchViewModel;
            dataprovider = container.GetService(typeof(IDataProvider)) as IDataProvider;
        }

        
        private async void ItemChosen(object sender, SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0)
            {
                Suggestion suggestion = (Suggestion)args.AddedItems[0];
                Item item = await dataprovider.GetItemById((int)suggestion.id);
                ItemViewParameters parameters = new ItemViewParameters();
                parameters.item = item;
                parameters.mainContent = mainContent;
                parameters.container = container;
                mainContent.Navigate(typeof(ItemView), parameters);
            }
        }
    }
}
