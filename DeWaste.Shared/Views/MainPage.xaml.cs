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
using DeWaste.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DeWaste.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        NavigationViewModel ViewModel;
        IServiceProvider container = ((App)App.Current).Container;

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = container.GetService(typeof(NavigationViewModel)) as NavigationViewModel;
            DataContext = ViewModel;
        }

        private void Navigation_Loaded(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(typeof(Views.SearchView), MainContent);
        }
        
        private void Navigation_Clicked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
                switch (args.InvokedItemContainer.Tag)
                {
                    case "SearchPage":
                        MainContent.Navigate(typeof(Views.SearchView), MainContent);
                        break;
                    case "ItemPage":
                        MainContent.Navigate(typeof(Views.ItemView));
                        break;
                }
        }
    }
}

