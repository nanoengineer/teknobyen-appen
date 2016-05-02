using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Teknobyen.Services.CredentialsService
{
    interface ICredentialsService
    {
        PasswordCredential GetUser();
        bool SaveUser(PasswordCredential user);
        bool DeleteUsers();
    }
}
