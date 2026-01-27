using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Auth0.OidcClient;

namespace BetAnalysisTool.Maui.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly Auth0Client _auth0Client;

        // Constructor Injection
        public LoginPageViewModel(Auth0Client client)
        {
            _auth0Client = client;
        }


        // Button click handler
        [RelayCommand]
        private async Task Login()
        {
            try
            {
                var loginResult = await _auth0Client.LoginAsync();

                if (!loginResult.IsError)
                {
                    // Access Token for API calls
                    var accessToken = loginResult.AccessToken;

                    // User Info
                    var name = loginResult.User?.Identity?.Name ?? "Unknown User";

                    await Shell.Current.DisplayAlert("Success", $"Logged in as {name}", "OK");

                    // NAVIGATE:
                    // The "///" allows us to switch from the Login route to the MainApp route
                    // effectively resetting the navigation stack so the back button doesn't go to Login
                    await Shell.Current.GoToAsync("//DashboardPage");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", loginResult.ErrorDescription, "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }   
        }
    }
}