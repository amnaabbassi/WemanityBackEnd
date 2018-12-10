using Infrastructure;
using Infrastructure.Repositories;
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
    class VoteServiceTest
    {
        Mock<VoteService> mockedVoteService;
        Mock<VoteRepository> mockedVoteRepository;
        Mock<SondageDbcontext> mockedDbcontext;
        
        [TestCase(true)]
        [TestCase(false)]
        public void AddVote_Test(bool isValid)
        {
            #region mock object

            mockedDbcontext = MockDbcontext();
            mockedVoteRepository = new Mock<VoteRepository>(mockedDbcontext.Object) { CallBase = true };
            mockedVoteService = new Mock<VoteService>(mockedVoteRepository.Object) { CallBase = true };
            #endregion

            #region acte

            mockedVoteRepository.Setup(c => c.AddVote(It.IsAny<Vote>())).Returns(
               isValid
                );
            var res = mockedVoteService.Object.AddVote(It.IsAny<Vote>());
            #endregion

            #region Assert
            if(isValid)
            {
                Assert.IsTrue(res);
            }
            else
            {
                Assert.IsFalse(res);
            }
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
