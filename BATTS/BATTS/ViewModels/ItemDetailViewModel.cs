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
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }




        bool isNew;
        public bool IsNew
        {
            get => isNew;
            set => SetProperty(ref isNew, value);
        }


        public Item item { get; set; }
        public ICommand SaveCommand { get; }

        public event EventHandler SaveComplete;

        public ItemDetailViewModel(Item todo, bool isNew)
        {
            IsNew = isNew;
            item = todo;

            SaveCommand = new Command(async () => await ExecuteSaveCommand());

            Title = IsNew ? "New To Do" : item.Name;
        }

        async Task ExecuteSaveCommand()
        {
            if (IsNew)
                await AzuCosmoDBManager.InsertItem(item);
            else
                await AzuCosmoDBManager.UpdateItem(item);

            SaveComplete?.Invoke(this, new EventArgs());
        }
    }
}
