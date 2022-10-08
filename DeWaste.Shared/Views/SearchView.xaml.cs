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

namespace DeWaste.Views
{
    public sealed partial class SearchView : Page
    {
        IDataProvider dataProvider;
        List<String> suggestions;
        public SearchView()
        {
            this.InitializeComponent();
            dataProvider = new DataProvider();
            suggestions = dataProvider.getSuggestions();
        }
        
        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            sender.ItemsSource = suggestions;
        }
    }
}
