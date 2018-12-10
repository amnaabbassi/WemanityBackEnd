using Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private IVoteRepository _voteRepository;
        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public bool AddVote(Vote vote)
        {
            return _voteRepository.AddVote(vote);
        }
    }
}
