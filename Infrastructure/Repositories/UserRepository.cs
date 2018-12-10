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

        public User GetUserByID(int id)
        {
            return _dbContext.USers.Find(id);
        }

        public List<User> GetUsers()
        {
            List<User> Users = new List<User>();
            string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            using (StreamReader r = new StreamReader(startupPath + @"\Data\Usersjson.json"))
            {
                var json = r.ReadToEnd();
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

        public string HashPassword(string Password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("Hello I'm here!!!!");

            string result = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return result;
        }

        public User Login(User user)
        {
            string token = string.Empty;
            string hashpassword = HashPassword(user.Password);

            User userexist = GetUsers().Where(s => s.Login == user.Login && s.Password == hashpassword).FirstOrDefault();
            if (userexist != default(User))
            {
                foreach (KeyValuePair<string, string> item in _loggedUser)
                {
                    if (item.Value == userexist.Login)
                        token = item.Key;
                }

                if (string.IsNullOrEmpty(token))
                {
                    token = Guid.NewGuid().ToString();
                    _loggedUser.Add(token, userexist.Login);
                }
            }
            user.Id = userexist.Id;
            user.LastNAme = userexist.LastNAme;
            user.Name = userexist.Name;
            user.Token = token;
            user.Password = "";
            return user;
        }
    }
}
