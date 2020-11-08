using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BATTS.Models;
using BATTS.Views;
using BATTS.ViewModels;
using BATTS.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Register : ContentPage
	{
        public List<UserDataModel> LoginDBRegister = new List<UserDataModel>();

        public bool IsValidEmail;
        public UserDataModel UserRegister = new UserDataModel();

        RegisterViewModel RVM;
        public Register ()
		{
			InitializeComponent ();
            BindingContext = RVM = new RegisterViewModel();
            RVM.Title = "Register Page";
          
        }

        async public void DoRegister(object sender, EventArgs e)
        {

           

            if (firstname.Text != ""  && firstname.Text != null && firstname.Text != "First Name")
            {

            }
            else
            {

            }
            bool emailexist = await RVM.EmailCheckAsync(email.Text);
            try
            {
                UserRegister.FirstName = firstname.Text.ToString();
                UserRegister.LastName = lastname.Text.ToString();
                UserRegister.Email = email.Text.ToString();
                IsValidEmail =RVM.IsValidEmail(UserRegister.Email);
                // UserRegister.Role = role.Text.ToString();
                UserRegister.Role = rolepicker.SelectedItem.ToString();
                UserRegister.Password = password.Text.ToString();
                UserRegister.ActiveUser = true;
            }
            catch
            {

            }
            if (IsValidEmail == false)
            {
                email.TextColor = Color.Red;
                notify.TextColor = Color.Red;
                email.Text = "Email Not Valid";
                notify.Text = "This email is not a valid email";
            }
            else if (emailexist == false) { 
           await RVM.RegisterNewUserAsync(UserRegister,true);
                notify.TextColor = Color.Green;
                email.TextColor = Color.Green;
                notify.Text = "User now registered go back to login";

            }
            else
            {
                email.TextColor = Color.Red;
                notify.TextColor = Color.Red;
                email.Text = "Email Exists";
                notify.Text = "This email is already registered";
               
            }
           

        }
        async public void GoBack(object sender, EventArgs e)
        {           
             await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));          

        }
    }
}