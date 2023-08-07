using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Implementations;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using Moq;

namespace backend.Test.src;

public class UserServiceTest
{
    private Mock<IUserRepo> _userRepositoryMock;
    private IMapper _mapper;
    private UserService _userService;

    public UserServiceTest()
    {
        // Arrange
        _userRepositoryMock = new Mock<IUserRepo>();
        _mapper = new MapperConfiguration(
            cfg => cfg.AddProfile<AutoMapperProfile>()
        ).CreateMapper();
        _userService = new UserService(_userRepositoryMock.Object, _mapper);
    }


        [Fact]
        public void UpdatePassword_ValidUser_ReturnsUpdatedUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Generate a new GUID
            string newPassword = "newPassword";
            var user = new User { Id = userId, Password = "oldPassword" };
            _userRepositoryMock.Setup(repo => repo.GetOneById(userId.ToString())).Returns(user);
            _userRepositoryMock.Setup(repo => repo.UpdatePassword(user, newPassword)).Returns(new User { Id = userId, Password = newPassword });

            // Act
            var result = _userService.UpdatePassword(userId.ToString(), newPassword); // Convert GUID to string for the method call

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(userId.ToString(), result.Id); // Parse the Id string back to Guid for the assertion
        }

        [Fact]
        public void UpdatePassword_InvalidUser_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Generate a new GUID
            string newPassword = "newPassword";
            _userRepositoryMock.Setup(repo => repo.GetOneById(userId.ToString())).Returns((User)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _userService.UpdatePassword(userId.ToString(), newPassword)); // Convert GUID to string for the method call
            Assert.Equal($"User with {userId} not found", exception.Message);
        }


}
