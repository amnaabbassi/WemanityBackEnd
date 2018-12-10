using Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BddPrejectTest.Steps
{
  [Binding]
  public class UsersSteps
  {
    RestClient _client = new RestClient("http://localhost:55251/api");
    [Given(@"I whould Get all users")]
    public void GivenIWhouldGetAllUsers()
    {
      var req = new RestRequest("users",Method.GET);
      ScenarioContext.Current.Add("getusers", _client.Execute(req));

    }

    [Then(@"the result should contains all users")]
    public void ThenTheResultShouldContainsAllUsers()
    {
      var  response=ScenarioContext.Current.Get<IRestResponse>("getusers");
      Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }

    [Given(@"I whould Login with (.*) and (.*)")]
    public void Login(string id,string pwd)
    {
      var req = new RestRequest("users/login", Method.POST);
      req.AddJsonBody(new User {Id=1, Login = id, Password = pwd });
      ScenarioContext.Current.Add("login", _client.Execute(req));

    }

    [Then(@"the result should be ok")]
    public void LoginResult()
    {
      var response = ScenarioContext.Current.Get<IRestResponse>("login");
      Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }
  }
}
