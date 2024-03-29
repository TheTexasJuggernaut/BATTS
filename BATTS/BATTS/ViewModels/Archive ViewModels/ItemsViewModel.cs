﻿using System;
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
        public ObservableCollection<UserDataModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }


        List<UserDataModel> todoItems;
        public List<UserDataModel> ToDoItems { get => todoItems; set => SetProperty(ref todoItems, value); }

        public ICommand RefreshCommand { get; }
        public ICommand CompleteCommand { get; }



        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<UserDataModel>();

            ToDoItems = new List<UserDataModel>();
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());


            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());



            MessagingCenter.Subscribe<NewItemPage, UserDataModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as UserDataModel;
                Items.Add(newItem);
                await AzuCosmoDBManager.InsertUserData(newItem);
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
                var items = await AzuCosmoDBManager.GetUserData();
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
                ToDoItems = await AzuCosmoDBManager.GetUserData();
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteCompleteCommand(UserDataModel item)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //await AzuCosmoDBManager.GetUserData();
                ToDoItems = await AzuCosmoDBManager.GetUserData();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}