using Models;
using Models.Interfaces;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;
using System.Linq;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private Dictionary<string, string> _loggedUser;
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _loggedUser = new Dictionary<string, string>();
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userRepository.GetUsers();
        }

        public User Login(User user)
        {
            return _userRepository.Login(user);
        }

    }
}
