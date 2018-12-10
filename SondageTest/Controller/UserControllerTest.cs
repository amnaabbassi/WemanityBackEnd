using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;

namespace SondageTest.Controller
{
  [TestFixture]
  public class UserControllerTest
  {
    private Mock<UsersController> mockcontroller;

    [Test]
    public void GetUSersTes()
    {
      #region moq object
      Mock<IUserService> mockedUserService = new Mock<IUserService>();
      mockcontroller = new Mock<UsersController>(mockedUserService.Object) { CallBase = true };
      IEnumerable<User> listuser = new List<User>
            {
                new User
                {
                    Id=Constants.ID_USER,
                    LastNAme=Constants.LASTNAME,
                    Name=Constants.USERNAME,
                    Login=Constants.LOGIN,
                    Password=Constants.PASSEWORD
                }
            };
      #endregion

      #region Act
      mockedUserService.Setup(c => c.GetAllUser()).Returns(listuser);
      var res = mockcontroller.Object.Get() as OkObjectResult;
      var ContentResult = res.Value as List<User>;
      #endregion

      #region Assert
      Assert.AreEqual(res.StatusCode, 200);
      Assert.AreEqual(ContentResult.Count, 1);
      Assert.AreEqual(ContentResult, listuser);
      #endregion

      #region verify
      mockedUserService.Verify(c => c.GetAllUser());
      #endregion
    }
  }
}
