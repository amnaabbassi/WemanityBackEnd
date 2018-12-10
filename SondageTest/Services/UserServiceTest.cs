using Infrastructure;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SondageTest.Services
{
  [TestFixture]
  class UserServiceTest
  {
    Mock<UserService> mockedUserService;
    Mock<UserRepository> mockedUserRepository;
    Mock<SondageDbcontext> mockedDbcontext;
    [Test]
    public void GetAllUser_Test()
    {
      #region mock object
      mockedDbcontext = MockDbcontext();
      mockedUserRepository = new Mock<UserRepository>(mockedDbcontext.Object);
      mockedUserService = new Mock<UserService>(mockedUserRepository.Object) { CallBase = true };
      #endregion

      #region acte
      var res = mockedUserService.Object.GetAllUser();
      #endregion

      #region Assert
      Assert.AreEqual(res.Count(), 1);
      Assert.AreEqual(res.First().Id, Constants.ID_USER_1);
      Assert.AreEqual(res.First().LastNAme, Constants.LASTNAME_1);
      Assert.AreEqual(res.First().Name, Constants.USERNAME_1);
      Assert.AreEqual(res.First().Login, Constants.LOGIN_1);
      #endregion

    }

    private Mock<SondageDbcontext> MockDbcontext()
    {

      var data = new List<User> {
                 new User
                {
                      Id=Constants.ID_USER,
                    LastNAme=Constants.LASTNAME,
                    Name=Constants.USERNAME,
                    Login=Constants.LOGIN,
                    Password=Constants.PASSEWORD
                }
            }.AsQueryable();

      var mockSet = new Mock<DbSet<User>>();
      mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
      mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
      mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
      mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

      var dbOptions = new DbContextOptionsBuilder<SondageDbcontext>()
               .UseInMemoryDatabase(databaseName: "Sondage")
               .Options;

      mockedDbcontext = new Mock<SondageDbcontext>(dbOptions);

      mockedDbcontext.Setup(c => c.USers).Returns(mockSet.Object);
      return mockedDbcontext;
    }
  }
}
