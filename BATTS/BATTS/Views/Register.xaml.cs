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
using System.Diagnostics;

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Register : ContentPage
	{
        #region Declarations
        public List<UserDataModel> LoginDBRegister = new List<UserDataModel>();
        public UserDataModel UserRegister = new UserDataModel();
        RegisterViewModel RVM;

        public bool IsValidEmail;       
        #endregion

        public Register ()
		{
			InitializeComponent ();
            BindingContext = RVM = new RegisterViewModel();
            RVM.Title = "Register Page";
          
        }

        #region Functions
        /// <summary>
        /// Controls the Notify Text on GUI, 2 inputs String and Int(1: Orange 2: Red 3: Green)
        /// </summary>
        /// <returns></returns>
        public void updateNotification(string statusInput, int statusType)
        {
            switch (statusType)
            {
                case 1:
                    notify.TextColor = Color.Orange;
                    break;
                case 2:
                    notify.TextColor = Color.Red;
                    break;
                case 3:
                    notify.TextColor = Color.Green;
                    break;
                default:
                    Debug.WriteLine("Recieved a unnacepted case"); ;
                    break;

            }

            notify.Text = statusInput;
        }

        /// <summary>
        /// Checks to see if user entris not a null or blank, returns true if null or blank is found
        /// </summary>
        /// <returns></returns>
        public static bool inputValid(string input)
        {
            return String.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Pull data from GUI
        /// </summary>
        /// <returns></returns>
        public void inputFromGUI()
        {
            try
            {
                UserRegister.FirstName = firstname.Text.ToString();
                UserRegister.LastName = lastname.Text.ToString();
                UserRegister.Email = email.Text.ToString();
                UserRegister.Role = rolepicker.SelectedItem.ToString();
                UserRegister.Password = password.Text.ToString();
                UserRegister.ActiveUser = true;


            }
            catch
            {
                Debug.WriteLine("Data Pull Failed");
            }
        }
        #endregion

        #region Event Trigger Functions
        async public void doRegister(object sender, EventArgs e)
        {
            if (!inputValid(firstname.Text) && !inputValid(lastname.Text) && !inputValid(email.Text) && !inputValid(password.Text))
            {

                bool emailexist = await RVM.EmailCheckAsync(email.Text);

                try
                {
                    inputFromGUI();
                    IsValidEmail = RVM.IsValidEmail(UserRegister.Email);

                    if (IsValidEmail == false)
                    {
                        email.TextColor = Color.Red;
                        email.Text = "Email Not Valid";

                        updateNotification("This email is not a valid email", 2);
                    }
                    else if (emailexist == false)
                    {


                        email.TextColor = Color.Green;
                        updateNotification("User now registered go back to login", 3);

                        await RVM.RegisterNewUserAsync(UserRegister, true);

                    }
                    else
                    {
                        email.TextColor = Color.Red;
                        email.Text = "Email Exists";
                        updateNotification("This email is already registered", 1);

                    }
                }
                catch
                {
                    updateNotification("A field is empty or null", 2);
                    Debug.WriteLine("Pull or Email Valid Failed");
                }
            }
            else
            {
                updateNotification("Entry(s) not valid", 2);
                Debug.WriteLine("Register Failed");
            }

                                          
        }
        async public void goBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));

        }
        #endregion
       
    }
}