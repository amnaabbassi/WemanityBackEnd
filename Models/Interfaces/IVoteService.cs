using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
    public interface IVoteService
    {
        bool AddVote(Vote vote);
    }
}
