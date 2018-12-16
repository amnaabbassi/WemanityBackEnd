using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;

namespace WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors("MyPolicy")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService;
    }

    /// <summary>
    /// GET list of user using api/Users endpoint
    /// </summary>
    /// <returns>returns IActionResult</returns>
    [HttpGet]
    public IActionResult Get()
    {
      var userList = _userService.GetAllUser();
      return Ok(userList);
    }

    /// <summary>
    /// login to authentification using  api/users/login
    /// </summary>
    /// <param name="user">the user</param>
    /// <returns>return ActionResult<User></User></returns>
    [HttpPost("login")]
    public ActionResult<User> Login(User user)
    {
      return _userService.Login(user);
    }
  }

}
