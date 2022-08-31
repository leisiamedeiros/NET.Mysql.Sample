using FluentAssertions;
using NET.Mysql.Sample.Application.UseCases.GetAllContatcs;
using NET.Mysql.Sample.IntegrationTests.Configuration;
using NET.Mysql.Sample.IntegrationTests.Shared;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace NET.Mysql.Sample.IntegrationTests.UseCases.GetAllContatcs
{
    [Binding]
    [Scope(Feature = "Get All Contacts")]
    public class GetAllContactsSteps : IClassFixture<ApplicationFixture>
    {
        private readonly ApplicationFixture _fixture;
        private HttpResponseMessage _result;

        public GetAllContactsSteps(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Given(@"I have an application up and running")]
        public void GivenIHaveAnApplicationUpAndRunning()
        {
            var result = _fixture.GetAsync("health/ready").Result;
            var healthResponse = _fixture.ReadResponseMessageAsync<HealthOutput>(result);

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            healthResponse.Result.Status.Should().Be("Healthy");
        }

        [When(@"I call the  endpoint '([^']*)' to get all contacts")]
        public void WhenICallTheEndpointToGetAllContacts(string path)
        {
            _result = _fixture.GetAsync(path).Result;
        }

        [Then(@"must have a list with more than one contact with success")]
        public void ThenMustHaveAListWithMoreThanOneContactWithSuccess()
        {
            _result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var contacts = _fixture.ReadResponseMessageAsync<GetAllContactsOutput>(_result);
            contacts.Result.Contacts.Should().HaveCountGreaterThan(1);
        }
    }
}
