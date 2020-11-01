using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BATTS.Models;
using BATTS.Services;

namespace BATTS.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        //public Item Item { get; set; }
        //public ItemDetailViewModel(Item item = null)
        //{
        //    Title = item?.Text;
        //    Item = item;
        //}




        bool isNew;
        public bool IsNew
        {
            get => isNew;
            set => SetProperty(ref isNew, value);
        }


        public UserDataModel item { get; set; }
        public ICommand SaveCommand { get; }

        public event EventHandler SaveComplete;

        public ItemDetailViewModel(UserDataModel Item, bool isNew)
        {
            IsNew = isNew;
            Item = item;

            SaveCommand = new Command(async () => await ExecuteSaveCommand());

            Title = IsNew ? "New To Do" : item.FirstName;
        }

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
