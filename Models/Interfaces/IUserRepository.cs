using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
  public interface IUserRepository:IRepository<User>, IAsyncRepository<User>
  {
        List<User> GetUsers();
        User Login(User user);
    }
}
