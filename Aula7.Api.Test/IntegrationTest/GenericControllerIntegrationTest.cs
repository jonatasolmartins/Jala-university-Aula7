using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Aula7.Api.Models;
using Aula7.Api.Test.Setup;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Aula7.Api.Test.IntegrationTest;

public class GenericControllerIntegrationTest : IClassFixture<GenericFactory>
{
    private readonly GenericFactory _genericFactory;

    
    public GenericControllerIntegrationTest(GenericFactory genericFactory)
    {
        _genericFactory = genericFactory;
    }

    [Fact]
    public async Task Try_CreateUser()
    {
        #region Arrange

        var client = _genericFactory.CreateClient();
        var repository = _genericFactory.TestRepository;
        var user = new User()
        {
            Id = 1,
            Name = "Jonatas"
        };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        #endregion

        #region Act

        var response = await client.PostAsync("api/Generic/SaveUser", content);
        var statusCode = response.StatusCode;
        var userCreated = await response.Content.ReadFromJsonAsync<User>();
        var userRepository = repository.GetById(1);

        #endregion

        #region Assert

        statusCode.Should().Be(HttpStatusCode.OK);
        userCreated?.Should().NotBeNull();
        userCreated.Should().BeEquivalentTo(user);
        userRepository.Should().NotBeNull();
        userRepository.Should().BeEquivalentTo(user);

        #endregion
    }
    
  
}