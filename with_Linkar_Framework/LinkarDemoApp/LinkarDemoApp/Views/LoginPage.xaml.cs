using LinkarCommon;
using LinkarDemoApp.ViewModels;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkarDemoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);  // Hide nav bar

            BindingContext = viewModel = new LoginViewModel();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await LoginAsync();
        }

        public async Task LoginAsync()
        {
            //CredentialsOptions credentialsOptions = new CredentialsOptions("94.130.139.208", 21301, "DEMODESKTOP", "54poimnmaa;", "DEMOLINKAR");
            //try
            //{
            //    await Task.Run(() => {
            //        App.linkarClt.Login(credentialsOptions);
            //        App.Current.MainPage = new MainPage();
            //        });
            //}
            //catch (Exception ex)
            //{
            //    await this.DisplayAlert("ERROR", App.GetException(ex), "OK");
            //}


            await viewModel.ExecuteLoginCommand();
            if (!string.IsNullOrEmpty(App.linkarClt.SessionID)) //NEWFRAMEWORK: Replace SessionId fro SessionID
                App.Current.MainPage = new MainPage();
        }
    }
}