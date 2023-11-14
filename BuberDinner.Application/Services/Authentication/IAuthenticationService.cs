using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstname, string lastname, string email, string password);
        AuthenticationResult Login(string email, string password);
    }
}
