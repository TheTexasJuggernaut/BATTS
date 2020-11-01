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
    public class LoginViewModel : BaseViewModel
    {
        //10/25/20 Creates a model that is shared by functions 

        public string email, password;

        List<UserDataModel> loginDB;
        public List<UserDataModel> LoginDB { get => loginDB; set => SetProperty(ref loginDB, value); }

        public ICommand RefreshCommand { get; }

        public LoginViewModel()
        {
            Title = "Browse";


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

        public async Task<bool> LoginCheckAsync(string Email, string Password)
        {
           
            try
            {
                LoginDB = await AzuCosmoDBManager.GetUserData();

                if (LoginDB.Exists(x => x.Email == Email))
                {
                    var User = LoginDB.Where(p => p.Email == Email).First();
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table
                    if (User.Password == Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


        }

        public async Task<string> GetUserIDAsync(string Email)
        {
            email = Email;


            try
            {
                LoginDB = await AzuCosmoDBManager.GetUserData();

                if (LoginDB.Exists(x => x.Email == email))
                {
                    var User = LoginDB.Where(p => p.Email == email).First();
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table

                    return User.Id;
                }
                else
                {
                    return "";
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }


        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {


                var items = await AzuCosmoDBManager.GetUserData();

                foreach (var item in items)
                {
                    //Items.Add(item);

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

                LoginDB = await AzuCosmoDBManager.GetUserData();
            }
            finally
            {
                IsBusy = false;
            }
        }



    }
}

