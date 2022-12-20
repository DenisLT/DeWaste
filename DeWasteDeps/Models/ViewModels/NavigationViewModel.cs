using System;
using System.Collections.Generic;
using System.Text;

namespace DeWaste.Models.ViewModels
{    
    public class NavigationViewModel : BindableBase
    {
        IServiceProvider container;

        public NavigationViewModel(IServiceProvider container)
        {
            this.container = container;
        }

        private bool _requestFailed = false;

        public bool FailedConnectToServer
        {
            get => _requestFailed;
            set => SetProperty(ref _requestFailed, value);
        }
    }
}
