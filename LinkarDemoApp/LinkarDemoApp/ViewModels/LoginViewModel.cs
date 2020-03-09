using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using LinkarDemoApp.Models;
using LinkarDemoApp.Views;
using System.Net.Http;
using LinkarDemoApp.Services;

namespace LinkarDemoApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginDataStore DataStore = DependencyService.Get<LoginDataStore>() ?? new LoginDataStore();
        public Command LoginCommand { get; set; }
        public Command LogoutCommand { get; set; }

        public LoginViewModel()
        {
            Title = "Login";
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());
        }

        public async Task ExecuteLoginCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //var login = await DataStore.LoginAsync();
                //if (login != null)
                //{
                //    App.Token = login.Token;
                //}
                string error = await DataStore.LoginAsync();
                if (!string.IsNullOrEmpty(error))
                {
                    await Application.Current.MainPage.DisplayAlert("ERROR", error, "OK");
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

        public async Task ExecuteLogoutCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var resp = await DataStore.LogoutAsync();
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
    }
}