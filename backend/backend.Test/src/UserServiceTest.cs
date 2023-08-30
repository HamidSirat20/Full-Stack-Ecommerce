using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Implementations;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using Moq;

namespace backend.Test.src
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepo> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPasswordService> _passwordServiceMock;
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepo>();
            _mapperMock = new Mock<IMapper>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _userService = new UserService(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _passwordServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateOne_ValidDto_Success()
        {
            // Arrange
            var createDto = new UserCreateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Avatar = "avatar-url",
                Password = "testpassword"
            };

            var createdUser = new User();
            var expectedCreatedUserDto = new UserReadDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Avatar = "avatar-url"
            };

            _mapperMock.Setup(mapper => mapper.Map<User>(createDto)).Returns(createdUser);
            _passwordServiceMock.Setup(
                service =>
                    service.HashPassword(
                        createDto.Password,
                        out It.Ref<string>.IsAny,
                        out It.Ref<byte[]>.IsAny
                    )
            );
            _userRepositoryMock
                .Setup(repo => repo.CreateOne(It.IsAny<User>()))
                .ReturnsAsync(createdUser);
            _mapperMock
                .Setup(mapper => mapper.Map<UserReadDto>(createdUser))
                .Returns(expectedCreatedUserDto);

            // Act
            var result = await _userService.CreateOne(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedCreatedUserDto, result);
            Assert.Equal(expectedCreatedUserDto.FirstName, result.FirstName);
            Assert.Equal(expectedCreatedUserDto.LastName, result.LastName);
            Assert.Equal(expectedCreatedUserDto.Email, result.Email);
            Assert.Equal(expectedCreatedUserDto.Avatar, result.Avatar);
        }

        [Fact]
        public async Task DeleteOneById_ItemExists_ReturnsTrue()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var foundItem = new User();
            _userRepositoryMock.Setup(repo => repo.GetOneById(itemId)).ReturnsAsync(foundItem);

            // Act
            var result = await _userService.DeleteOneById(itemId);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.DeleteOneById(foundItem), Times.Once);
        }

        [Fact]
        public async Task DeleteOneById_ItemNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetOneById(itemId))!.ReturnsAsync((User)null);

            // Act and Assert
            await Assert.ThrowsAsync<CustomErrorHandler>(() => _userService.DeleteOneById(itemId));
            _userRepositoryMock.Verify(repo => repo.DeleteOneById(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task GetOneById_ItemExists_ReturnsDto()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var foundEntity = new User();
            var expectedDto = new UserReadDto();

            _userRepositoryMock.Setup(repo => repo.GetOneById(itemId)).ReturnsAsync(foundEntity);
            _mapperMock.Setup(mapper => mapper.Map<UserReadDto>(foundEntity)).Returns(expectedDto);

            // Act
            var result = await _userService.GetOneById(itemId);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedDto, result);
        }

    }
}
