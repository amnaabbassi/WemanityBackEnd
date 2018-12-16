using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces;

namespace WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors("MyPolicy")]
  public class VoteController : ControllerBase
  {
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService)
    {
      _voteService = voteService;
    }

    /// <summary>
    /// insert vote to database in our case json file
    /// </summary>
    /// <param name="vote"></param>
    /// <returns></returns>
    [HttpPost("voteuser")]
    public ActionResult<bool> AddUserVote([FromBody] Vote vote)
    {
      return _voteService.AddVote(vote);
    }
  }
}
