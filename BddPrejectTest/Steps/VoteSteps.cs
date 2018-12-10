using Models.Interfaces;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BddPrejectTest.Steps
{
  [Binding]
  public class VoteSteps
  {
    RestClient _client = new RestClient("http://localhost:55251/api");

    [Given(@" I whould add vote  with (.*) and (.*) and (.*)")]
    public void GivenIWhouldAddAVoteWithAndFRANCEAnd(int p0,string ctr, int p1)
    {
      var req = new RestRequest("vote /voteuser", Method.POST);
      req.AddJsonBody(new Vote { Id=p0,country=ctr,IdUser=p1});
      ScenarioContext.Current.Add("addvote", _client.Execute(req));
    }


    [Then(@"the result for adding vote  should be ok")]
    public void AddVoteResult()
    {
      var response = ScenarioContext.Current.Get<IRestResponse>("addvote");
      Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }
  }
}
