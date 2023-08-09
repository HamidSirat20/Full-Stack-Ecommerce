using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Implementations;
using backend.Domain.src.Common;
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
        _userRepositoryMock
            .Setup(repo => repo.UpdatePassword(user, newPassword))
            .Returns(new User { Id = userId, Password = newPassword });

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
        var exception = Assert.Throws<Exception>(
            () => _userService.UpdatePassword(userId.ToString(), newPassword)
        ); // Convert GUID to string for the method call
        Assert.Equal($"User with {userId} not found", exception.Message);
    }

    [Fact]
    public void CreateOne_ValidEntity_ReturnsMappedDto()
    {
        // Arrange
        var entity = new User(); // Replace with your actual entity type
        var dto = new UserDto(); // Replace with your actual DTO type

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.CreateOne(entity)).Returns(entity);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<UserDto>(entity)).Returns(dto);

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act
        var result = service.CreateOne(entity);

        // Assert
        Assert.Equal(dto, result);
    }

    [Fact]
    public void GetOneById_ValidId_ReturnsMappedDto()
    {
        // Arrange
        var entityId = "valid_id";
        var entity = new User(); // Replace with your actual entity type
        var dto = new UserDto(); // Replace with your actual DTO type

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns(entity);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<UserDto>(entity)).Returns(dto);

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act
        var result = service.GetOneById(entityId);

        // Assert
        Assert.Equal(dto, result);
    }

    [Fact]
    public void DeleteOneById_ExistingId_ReturnsTrue()
    {
        // Arrange
        var entityId = "existing_id";

        var entity = new User(); // Replace with your actual entity type
        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns(entity);

        var mapperMock = new Mock<IMapper>();

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act
        var result = service.DeleteOneById(entityId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteOneById_NonExistingId_ThrowsException()
    {
        // Arrange
        var entityId = "non_existing_id";

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns((User)null);

        var mapperMock = new Mock<IMapper>();

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act and Assert
        Assert.Throws<Exception>(() => service.DeleteOneById(entityId));
    }

    [Fact]
    public void GetOneById_ExistingId_ReturnsMappedDto()
    {
        // Arrange
        var entityId = "existing_id";
        var entity = new User(); // Replace with your actual entity type
        var dto = new UserDto(); // Replace with your actual DTO type

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns(entity);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<UserDto>(entity)).Returns(dto);

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act
        var result = service.GetOneById(entityId);

        // Assert
        Assert.Equal(dto, result);
    }

    [Fact]
    public void GetOneById_NonExistingId_ThrowsException()
    {
        // Arrange
        var entityId = "non_existing_id";

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns((User)null);

        var mapperMock = new Mock<IMapper>();

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act and Assert
        Assert.Throws<Exception>(() => service.GetOneById(entityId));
    }

    [Fact]
    public void UpdateOneById_ExistingEntity_ReturnsUpdatedMappedDto()
    {
        // Arrange
        var entityId = "existing_id";
        var updatedEntity = new User(); // Replace with your actual entity type
        var updatedDto = new UserDto(); // Replace with your actual DTO type

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns(new User());

        repoMock
            .Setup(repo => repo.UpdateOneById(It.IsAny<User>(), It.IsAny<User>()))
            .Returns(updatedEntity);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<UserDto>(updatedEntity)).Returns(updatedDto);

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act
        var result = service.UpdateOneById(entityId, new UserDto());

        // Assert
        Assert.Equal(updatedDto, result);
    }

    [Fact]
    public void UpdateOneById_NonExistingEntity_ThrowsException()
    {
        // Arrange
        var entityId = "non_existing_id";

        var repoMock = new Mock<IBaseRepo<User>>();
        repoMock.Setup(repo => repo.GetOneById(entityId)).Returns((User)null);

        var mapperMock = new Mock<IMapper>();

        var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

        // Act and Assert
        Assert.Throws<Exception>(() => service.UpdateOneById(entityId, new UserDto()));
    }

    [Fact]
        public void GetAll_ReturnsMappedDtos()
        {
            // Arrange
            var queryParameters = new QueryParameters();
            var entities = new List<User> // Replace with your actual entity type
            {
                new User(),
                new User(),
                // Add more entities as needed
            };

            var repoMock = new Mock<IBaseRepo<User>>();
            repoMock.Setup(repo => repo.GetAll(queryParameters)).Returns(entities);

            var mappedDtos = new List<UserDto>
            {
                new UserDto(),
                new UserDto(),
                // Add more DTOs as needed
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>())).Returns(mappedDtos);

            var service = new BaseService<User, UserDto>(repoMock.Object, mapperMock.Object);

            // Act
            var result = service.GetAll(queryParameters);

            // Assert
            Assert.Equal(mappedDtos, result);
        }
}
