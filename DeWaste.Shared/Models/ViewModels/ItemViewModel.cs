using DeWaste.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeWaste.Models.ViewModels
{
    class ItemViewModel : BindableBase
    {
        private Item item;

        
        public ItemViewModel()
        {
            item = new Item();
            item.img = "/Assets/Images/logo.png";
            item.description = "Go to search and search for something in order to display.";
            item.name = "Example item";
            updateUI();
        }

        private void updateUI()
        {
            OnPropertyChanged("ItemDescription");
            OnPropertyChanged("ItemName");
            OnPropertyChanged("ItemImage");
        }

        public void SetItem(Item item)
        {
            this.item = item;
            item.img = "/Assets/Images/Items/" + item.img; 
            updateUI();
        }

        public string ItemDescription
        {
            get => item.description;
            set
            {
                var temp = item.description;
                SetProperty(ref temp, value);
                item.description = temp;
            }
        }
        public string ItemName
        {
            get => item.name;
            set
            {
                var temp = item.name;
                SetProperty(ref temp, value);
                item.name = temp;
            }
        }

        public string ItemImage
        {
            get => item.img;
            set
            {
                var temp = item.img;
                SetProperty(ref temp, value);
                item.img = temp;
            }
        }
    }
}
