using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BATTS.Models;
using BATTS.Services;

namespace BATTS.ViewModels
{
    public class CompletedItemViewModel : BaseViewModel
    {
        List<UserDataModel> completedToDos; //Update tags 
        public List<UserDataModel> CompletedToDos { get => completedToDos; set => SetProperty(ref completedToDos, value); }

        public ICommand RefreshCommand { get; }

        public CompletedItemViewModel(UserDataModel Item, bool isNew)
        {
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            Title = "User Data";

            IsNew = isNew;
            Item = item;

            SaveCommand = new Command(async () => await ExecuteSaveCommand());

            Title = IsNew ? "New To Do" : item.FirstName;
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CompletedToDos = await AzuCosmoDBManager.GetUserData();
            }
            finally
            {
                IsBusy = false;
            }


        }

        bool isNew;
        public bool IsNew
        {
            get => isNew;
            set => SetProperty(ref isNew, value);
        }


        public UserDataModel item { get; set; }
        public ICommand SaveCommand { get; }

        public event EventHandler SaveComplete;

        //public ItemDetailViewModel(Item Item, bool isNew)
        //{
        //    IsNew = isNew;
        //    Item = item;

        //    SaveCommand = new Command(async () => await ExecuteSaveCommand());

        //    Title = IsNew ? "New To Do" : item.Name;
        //}

        async Task ExecuteSaveCommand()
        {
            if (IsNew)
                await AzuCosmoDBManager.InsertUserData(item);
            else
                await AzuCosmoDBManager.UpdateUserData(item);

            SaveComplete?.Invoke(this, new EventArgs());
        }
    }
}
