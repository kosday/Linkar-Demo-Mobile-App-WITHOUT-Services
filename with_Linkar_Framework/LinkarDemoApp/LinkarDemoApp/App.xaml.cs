using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LinkarDemoApp.Views;
using System.Globalization;

//NEWFRAMEWORK: Linkar Framework Libraries
using Linkar.Functions.Persistent.MV;
using Linkar;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LinkarDemoApp
{
    public partial class App : Application
    {
        public static LinkarClient linkarClt = new LinkarClient(); //NEWFRAMEWORK: Replace LinkarClt for LinkarClient

        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static string GetException(Exception ex)
        {
            string msg = "";
            if (ex.GetType() == typeof(LkException))
            {
                LkException lkex = (LkException)ex;
                msg = "LINKAR EXCEPTION ERROR";
                if (lkex.ErrorCode == LkException.ERRORCODE.C0003)
                    msg += "\r\nERROR CODE: " + lkex.ErrorCode +
                           "\r\nERROR MESSAGE: " + lkex.ErrorMessage +
                           "\r\nInternal ERROR CODE: " + lkex.InternalCode +
                           "\r\nInternal ERROR MESSAGE: " + lkex.InternalMessage;
                else
                    msg += "\r\nERROR CODE: " + lkex.ErrorCode +
                           "\r\nERROR MESSAGE: " + lkex.ErrorMessage;
            }
            else if (ex.GetType() == typeof(System.Net.Sockets.SocketException))
            {
                System.Net.Sockets.SocketException soex = (System.Net.Sockets.SocketException)ex;
                msg = "SOCKET EXCEPTION ERROR\r\n" + soex.Message;
            }
            else
            {
                msg = "EXCEPTION ERROR\r\n" + ex.Message;
            }

            return msg;
        }
    }

    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = "";
            if (value != null)
            {
                ret = ((DateTime)value).ToString("dd'/'MM'/'yyyy");
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
