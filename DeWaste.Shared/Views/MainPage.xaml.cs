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
using DeWaste.Models.DataModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DeWaste
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        NavigationViewModel ViewModel;
        IServiceProvider container;

        public MainPage()
        {
            this.InitializeComponent();
        }
        
        private void Navigation_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.container = container;
            parameters.mainContent = MainContent;
            MainContent.Navigate(typeof(Views.SearchView), parameters);
        }

        private void Navigation_Clicked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag)
            {
                case "SearchPage":
                    NavigationParameters parameters = new NavigationParameters();
                    parameters.container = container;
                    parameters.mainContent = MainContent;
                    MainContent.Navigate(typeof(Views.SearchView), parameters);
                    break;
                case "ItemPage":
                    ItemViewParameters parametersItem = new ItemViewParameters();
                    parametersItem.mainContent = MainContent;
                    parametersItem.container = container;
                   
                    MainContent.Navigate(typeof(Views.ItemView), parametersItem);
                    break;
            }
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            container = (IServiceProvider)e.Parameter;
            ViewModel = container.GetService(typeof(NavigationViewModel)) as NavigationViewModel;
            DataContext = ViewModel;
        }
    }
}
