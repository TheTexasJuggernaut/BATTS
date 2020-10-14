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
        List<Item> completedToDos;
        public List<Item> CompletedToDos { get => completedToDos; set => SetProperty(ref completedToDos, value); }

        public ICommand RefreshCommand { get; }

        public CompletedItemViewModel()
        {
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            Title = "Completed";
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CompletedToDos = await AzuCosmoDBManager.GetCompletedItems();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
