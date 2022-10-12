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
using DeWaste.DataFiles;
using System.Threading.Tasks;

namespace DeWaste.Views
{
    public sealed partial class SearchView : Page
    {
        IDataProvider dataProvider;
        List<String> suggestions;
        Frame mainContent;
        
        
        public SearchView()
        {
            this.InitializeComponent();
            dataProvider = new DataProvider();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainContent = (Frame)e.Parameter;
        }

        private async void Load_Suggestions(object sender, RoutedEventArgs e)
        {
            suggestions = await dataProvider.getSuggestionsAsync();
        }


        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            sender.ItemsSource = suggestions;
        }
        
        private void Item_Chosen(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Item item = dataProvider.get(args.QueryText);
            mainContent.Navigate(typeof(ItemView), item);
        }
    }
}
