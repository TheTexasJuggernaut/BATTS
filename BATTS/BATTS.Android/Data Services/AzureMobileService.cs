using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using BATTS.Droid.DataModel;

namespace BATTS.Droid.DataServices
{

    public class AzureMobileService
    {
        public MobileServiceClient Client { get; private set; }
        private IMobileServiceSyncTable<UserData> debtTable;
        //https://github.com/mindofai/mobileservicebackup/tree/master/MobileAppServiceBackup/MobileAppServiceBackup
        //https://mindofai.github.io/Create-a-Backend-for-Xamarin.Forms-using-Azure-Mobile-App-Service/



       public async Task Initialize()
        {
            Client = new MobileServiceClient("https://batssdb.azurewebsites.net");

            var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "debtsync.db");

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<UserData>();

            await Client.SyncContext.InitializeAsync(store);

            debtTable = Client.GetSyncTable<UserData>();
        }


        private async Task SyncDebt()
        {
            await debtTable.PullAsync("allDebt", debtTable.CreateQuery());
            await Client.SyncContext.PushAsync();
        }

        private async Task<List<UserData>> GetAllDebts()
        {
            await SyncDebt();
            return await debtTable.ToListAsync();
        }

        private async Task<bool> AddDebt(UserData debt)
        {
            try
            {
                await debtTable.InsertAsync(debt);
                await SyncDebt();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> UpdateDebt(UserData debt)
        {
            try
            {
                debt.Name = "name";
                await debtTable.UpdateAsync(debt);
                await SyncDebt();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}