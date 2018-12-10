using System;
using TechTalk.SpecFlow;

namespace SondageBdd.Steps
{
    [Binding]
    public class UserFeatureSteps
    {
        [Given(@"I Get all users")]
        public void GivenIGetAllUsers()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should contains a list of users")]
        public void ThenTheResultShouldContainsAListOfUsers()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
