using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using prototype.Models;
using prototype.Pages;
using prototype.ViewModels;
using prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GamePage());
        }
    }
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        #region Commands
        [ICommand]
        async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var userDetails = new UserBasicInfo();
                userDetails.Email = Email;
                userDetails.FullName = "Gamer";

                // Student Role, Teacher Role
                if (Email.ToLower().Contains(".edu"))
                {
                    userDetails.RoleID = (int)RoleDetails.Student;
                    userDetails.RoleText = "College-board";
                }
                else if (Email.ToLower().Contains(".com"))
                {
                    userDetails.RoleID = (int)RoleDetails.User;
                    userDetails.RoleText = "World-board";
                }
                

                // calling api 


                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                {
                    Preferences.Remove(nameof(App.UserDetails));
                }

                string userDetailStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailStr);
                App.UserDetails = userDetails;
               

            }


        }
        #endregion
                
       
    }

    

    
}