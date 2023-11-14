using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User does not exist");
            }

            if(user.Password != password)
            {
                throw new Exception("Invalid password");
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token
            );
        }

        public AuthenticationResult Register(string firstname, string lastname, string email, string password)
        {
            //check if user is already registered
            if(_userRepository.GetUserByEmail(email) != null)
            {
                throw new Exception("User already exists");
            }


            //create user 
            var user = new User
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Password = password
            };

            _userRepository.AddUser(user);

            //create jwt token
            string token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}
