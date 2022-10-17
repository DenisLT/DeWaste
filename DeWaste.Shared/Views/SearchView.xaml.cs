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

namespace DeWaste.Views
{
    public sealed partial class SearchView : Page
    {
        List<String> suggestions;
        Frame mainContent;
        public SearchViewModel ViewModel;
        
        
        public SearchView()
        {
            this.InitializeComponent();
            ViewModel = new SearchViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainContent = (Frame)e.Parameter;
        }

        
        private void ItemChosen(object sender, SelectionChangedEventArgs args)
        {
            mainContent.Navigate(typeof(ItemView), args.AddedItems[0]);
        }
    }
}
