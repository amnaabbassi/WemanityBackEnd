using Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Infrastructure.Repositories
{
  public class VoteRepository : EfRepository<Vote>, IVoteRepository
  {
    public VoteRepository(SondageDbcontext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Add vote
    /// </summary>
    /// <param name="vote">Vote</param>
    /// <returns>return a boolean</returns>
    public virtual bool AddVote(Vote vote)
    {
      bool isVoteAdded = false;
      try
      {
        string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        List<Vote> listvote = new List<Vote>();
        int lastId = 0;

        using (StreamReader r = new StreamReader(startupPath + @"\Data\Vote.json"))
        {
          var initialJson = r.ReadToEnd();
          var items = JsonConvert.DeserializeObject<List<Vote>>(initialJson);
          foreach (var item in items)
          {
            var newvote = new Vote();
            newvote.Id = item.Id;
            newvote.country = item.country;
            newvote.IdUser = item.IdUser;
            listvote.Add(newvote);
            lastId = newvote.Id;
          }
          listvote.Add(new Vote { Id = lastId + 1, country = vote.country, IdUser = vote.IdUser });
        }

        string json = JsonConvert.SerializeObject(listvote.ToArray());

        //write string to file
        System.IO.File.WriteAllText((startupPath + @"\Data\Vote.json"), json);
        isVoteAdded = true;
      }
      catch (Exception ex)
      {
        throw;
      }
      return isVoteAdded;
    }
  }
}
