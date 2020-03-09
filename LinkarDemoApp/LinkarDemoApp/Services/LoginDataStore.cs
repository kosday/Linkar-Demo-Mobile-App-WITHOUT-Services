using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkarClient;
using LinkarCommon;
using LinkarDemoApp.Models;

namespace LinkarDemoApp.Services
{
    public class LoginDataStore
    {        

        public LoginDataStore()
        {

        }

        public async Task<string> LoginAsync()
        {
            short EntryPointPort = 11200;
            CredentialsOptions credentialsOptions = new CredentialsOptions("<Linkar server IP address>", "<Entry Point Name>", EntryPointPort, "<User Name>", "<Password>");
            return await Task.Run(() =>
            {
                try
                {
                    App.linkarClt.Login(credentialsOptions);
                    return null;
                }
                catch(Exception ex)
                {
                    return App.GetException(ex);
                }
            });

            //var json = await client.GetStringAsync($"api/Linkar/Login?user=linkar&pass=linkar");
            //return await Task.Run(() => JsonConvert.DeserializeObject<Login>(json));
        }

        public async Task<string> LogoutAsync()
        {
            if (!string.IsNullOrEmpty(App.linkarClt.SessionId))
            {
                return await Task.Run(() =>
                {
                    try
                    {
                        App.linkarClt.Logout();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return App.GetException(ex);
                    }
                });
            }
            else
                return null;

            //if (App.Token != null)
            //{
            //    var json = await client.GetStringAsync($"api/Linkar/Logout?token=" + App.Token);
            //    return await Task.Run(() => JsonConvert.DeserializeObject<string>(json));
            //}

            //return null;
        }
    }
}