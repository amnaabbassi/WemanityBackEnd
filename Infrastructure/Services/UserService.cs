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

    /// <summary>
    /// Get all user
    /// </summary>
    /// <returns>return list of user</returns>
    public IEnumerable<User> GetAllUser()
    {
      return _userRepository.GetUsers();
    }

    /// <summary>
    /// login user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>return used loged</returns>
    public User Login(User user)
    {
      string token = string.Empty;
      user.Password = HashPassword(user.Password);
      return _userRepository.Login(user);
    }

    /// <summary>
    /// hash password of login user
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private string HashPassword(string password)
    {
      byte[] salt = Encoding.ASCII.GetBytes("Hello I'm here!!!!");

      string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
      password: password,
      salt: salt,
      prf: KeyDerivationPrf.HMACSHA1,
      iterationCount: 10000,
      numBytesRequested: 256 / 8));
      return hashedPassword;
    }
  }
}
