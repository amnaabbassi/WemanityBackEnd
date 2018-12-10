using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Controllers;

namespace SondageTest.Controller
{
    [TestFixture]
    public class VoteControllerTest
    {
        private Mock<VoteController> mockcontroller;

        [TestCase(true)]
        [TestCase(false)]
        public void AddVotesTes(bool isValid)
        {
            #region moq object
            Mock<IVoteService> mockedVoteService = new Mock<IVoteService>();
            mockcontroller = new Mock<VoteController>(mockedVoteService.Object) { CallBase = true };
            #endregion

            #region Act
            mockedVoteService.Setup(c => c.AddVote(It.IsAny<Vote>())).Returns(isValid);
            var res = mockcontroller.Object.AddUserVote(It.IsAny<Vote>());
            var ContentResult = res.Value;
            #endregion

            #region Assert
            if (isValid)
            {
                Assert.IsTrue(res.Value);
            }
            else
            {
                Assert.IsFalse(res.Value);
            }

            #endregion

            #region verify
            mockedVoteService.Verify(c => c.AddVote(It.IsAny<Vote>()));
            #endregion
        }
    }
}
