using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Teknobyen.Services.CredentialsService
{
    class CredentialsService : ICredentialsService
    {
        private string resourceName = App.APPID;

        public static CredentialsService Instance { get; }
        static CredentialsService()
        {
            // implement singleton pattern
            Instance = Instance ?? new CredentialsService();
        }


        /// <summary>
        /// Deletes the currently stored username and password for the laundry login
        /// </summary>
        /// <returns>Bool indicating success</returns>
        public bool DeleteUsers()
        {
            try
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                var credentialList = vault.FindAllByResource(resourceName);
                foreach (var credential in credentialList)
                {
                    vault.Remove(credential);
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return false;
            
        }

        /// <summary>
        /// Returns the currently stored user from credentials locker
        /// as a UserModel
        /// </summary>
        /// <returns>Currently stored laundry credentials</returns>
        public PasswordCredential GetUser()
        {
            try
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                var credentialList = vault.FindAllByResource(resourceName);
                if (credentialList.Count > 0)
                {
                    var credentialsWithPassword = vault.Retrieve(App.APPID, credentialList.First().UserName);
                    return credentialsWithPassword;
                }
            }
            catch (Exception)
            {
                return null;
            }
            
            return null;
        }

        /// <summary>
        /// Clears the credentialslocker and saves the username and password to the credentials locker
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Bool indicating success</returns>
        public bool SaveUser(PasswordCredential user)
        {
            try
            {
                DeleteUsers();

                var vault = new PasswordVault();
                vault.Add(user);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return false;
        }
    }
}
