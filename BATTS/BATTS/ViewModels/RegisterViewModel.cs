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
using System.Linq;


namespace BATTS.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public string email;
        public string password;
        List<UserDataModel> loginDB;
        public List<UserDataModel> LoginDB { get => loginDB; set => SetProperty(ref loginDB, value); }

        public RegisterViewModel()
        {
            Title = "Register";

            LoginDB = new List<UserDataModel>();
            // RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            // LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());              
            LoginDB.Clear();


            //MessagingCenter.Subscribe<NewItemPage, UserDataModel>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as UserDataModel;
            //    //Items.Add(newItem);
            //    await AzuCosmoDBManager.InsertUserData(newItem);
            //});
        }
        public async Task RegisterNewUserAsync(UserDataModel NewUser, bool IsNew)
        {
           

            try
            {
                if (IsNew)
                    await AzuCosmoDBManager.InsertUserData(NewUser);
                else
                    await AzuCosmoDBManager.UpdateUserData(NewUser);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                
            }


        }
        public async Task<bool> EmailCheckAsync(string Email)
        {
            email = Email;
            

            try
            {
                LoginDB = await AzuCosmoDBManager.GetUserData();

                if (LoginDB.Exists(x => x.Email == email))
                {
                    var User = LoginDB.Where(p => p.Email == email).First();
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table
                    return true;
                }
               

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
              
            }
            return false;


        }
    }
}
