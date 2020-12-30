using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkarDemoApp.Models;

//NEWFRAMEWORK: Linkar Framework Libraries
using Linkar;

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
            CredentialOptions credentialsOptions = new CredentialOptions("<Linkar server IP address>", "<Entry Point Name>", EntryPointPort, "<User Name>", "<Password>", "<Language>","<FreeText>"); //NEWFRAMEWORK: Replace CredentialsOptions for CredentialOptions, add "<Language>" and "<FreeText>"
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
            if (!string.IsNullOrEmpty(App.linkarClt.SessionID)) //NEWFRAMEWORK: Replace SessionId for SessionID
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