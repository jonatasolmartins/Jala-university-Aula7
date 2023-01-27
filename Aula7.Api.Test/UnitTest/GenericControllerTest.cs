using Aula7.Api.Controllers;
using Aula7.Api.Interfaces;
using Aula7.Api.Models;
using Aula7.Api.Repository;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Aula7.Api.Test.UnitTest;

public class GenericControllerTest
{
    private readonly IFixture _fixture;
    public GenericControllerTest()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Fact]
    public void Try_CreateUser()
    {
        #region Arrange

        var user = new User()
        {
            Id = 1,
            Name = "Raul"
        };
        
        var userRepository = _fixture.Freeze<Mock<IUserRepository>>();
        userRepository.Setup(x => x.GetById(1)).Returns(user).Verifiable();
        
        var unitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
        unitOfWork.Setup(x => x.UserRepository).Returns(userRepository.Object);
        
        var genericController = _fixture.Build<GenericController>().OmitAutoProperties().Create();
        #endregion
    
        #region Act
    
        var response = genericController.GetUserById(user.Id);
        
        var userResponse = response.Result as OkObjectResult;

        #endregion

        #region Assert

        userResponse.Value.Should().NotBeNull();
        userResponse.Value.Should().Be(user);
        userRepository.Verify(x => x.GetById(1), Times.Once);

        #endregion
    }
}