using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Models;
using Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure
{
  public class UserRepository : EfRepository<User>, IUserRepository
  {
    private Dictionary<string, string> _loggedUser;

    public UserRepository(SondageDbcontext dbContext) : base(dbContext)
    {
      _loggedUser = new Dictionary<string, string>();
    }

    /// <summary>
    /// Get the user data by id
    /// </summary>
    /// <param name="id">id of user</param>
    /// <returns>return user</returns>
    public User GetUserByID(int id)
    {
      return _dbContext.USers.Find(id);
    }

    /// <summary>
    /// Get a list of user
    /// </summary>
    /// <returns>return a list of user from database in our case from a json file</returns>
    public List<User> GetUsers()
    {
      var Users = new List<User>();
      string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
      using (StreamReader r = new StreamReader(startupPath + @"\Data\Usersjson.json"))
      {
        string json = r.ReadToEnd();
        var items = JsonConvert.DeserializeObject<List<User>>(json);

        foreach (var item in items)
        {
          User newuser = new User();
          newuser.Id = item.Id;
          newuser.Name = item.Name;
          newuser.LastNAme = item.LastNAme;
          newuser.Login = item.Login;
          newuser.Password = item.Password;
          Users.Add(newuser);
        }
      }
      return Users;
    }

    /// <summary>
    /// Get the user logged
    /// </summary>
    /// <param name="user">authenticated user</param>
    /// <returns>return logged user</returns>
    public User Login(User user)
    {
      string token = string.Empty;
      User exestingUser = GetUsers().Where(s => s.Login == user.Login && s.Password == user.Password).FirstOrDefault();

      if (exestingUser != null)
      {
        foreach (KeyValuePair<string, string> item in _loggedUser)
        {
          if (item.Value == exestingUser.Login)
            token = item.Key;
        }

        if (string.IsNullOrEmpty(token))
        {
          token = Guid.NewGuid().ToString();
          _loggedUser.Add(token, exestingUser.Login);
        }

        exestingUser.Token = token;
        exestingUser.Password = string.Empty;
      }
     
      return exestingUser;
    }

  }
}
