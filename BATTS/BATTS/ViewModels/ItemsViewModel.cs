using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.Forms;
using BATTS.Models;
using BATTS.Views;
using BATTS.Services;

namespace BATTS.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }


        List<Item> todoItems;
        public List<Item> ToDoItems { get => todoItems; set => SetProperty(ref todoItems, value); }

        public ICommand RefreshCommand { get; }
        public ICommand CompleteCommand { get; }



        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();

            ToDoItems = new List<Item>();
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());


            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());



            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }



        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ToDoItems = await AzuCosmoDBManager.GetItems();
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteCompleteCommand(Item item)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await AzuCosmoDBManager.CompleteItem(item);
                ToDoItems = await AzuCosmoDBManager.GetItems();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}