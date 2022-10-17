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
using Microsoft.UI.Xaml.Media.Imaging;
using DeWaste.Models.DataModels;
using DeWaste.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace DeWaste.Views
{
    public sealed partial class ItemView : Page
    {
        ItemViewModel ViewModel;

        public ItemView()
        {
            this.InitializeComponent();
            var container = ((App)App.Current).Container;
            ViewModel = ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(BindableBase)) as ItemViewModel;
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                return;
            }
            ViewModel.SetItem((Item)e.Parameter);
        }
    }
}
