using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
    public interface IVoteRepository:IRepository<Vote>,IAsyncRepository<Vote>
    {
        bool AddVote(Vote vote);
    }
}
