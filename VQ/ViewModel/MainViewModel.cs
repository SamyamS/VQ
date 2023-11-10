using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VQ.ViewModel {
    public partial class MainViewModel: ObservableObject 
    {

        public MainViewModel() {
            Items = new ObservableCollection<string>();
        }

        [ObservableProperty]
        string text;

        [ObservableProperty]
        ObservableCollection<string> items;

        [RelayCommand]
        void add() {
            // add item to the query 

            if(string.IsNullOrEmpty(Text))
                return;


            Items.Add(Text);

            Text = string.Empty;
        }

        [RelayCommand]
        void delete(string queueItem) {
            // delete the items from the queue 
            if(Items.Contains(queueItem)) {
                Items.Remove(queueItem);
            }
        }
    }
}
