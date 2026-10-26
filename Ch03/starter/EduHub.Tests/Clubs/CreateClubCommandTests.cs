using EduHub.Application.Clubs;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;

namespace EduHub.Tests.Clubs;

public class CreateClubCommandTests
{
    [Fact]
    public async Task CreateClub_WithValidName_ReturnsCreatedClub()
    {
        // Arrange
        var mockRepo = new Mock<IClubRepository>();
        var handler = new CreateClubCommandHandler(mockRepo.Object);
        var command = new CreateClubCommand("Tech Club", "Câu lạc bộ công nghệ");

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Tech Club", result.Name);
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Club>(), default), Times.Once);
    }

    [Fact]
    public async Task CreateClub_WithEmptyName_ThrowsException()
    {
        // Arrange
        var mockRepo = new Mock<IClubRepository>();
        var handler = new CreateClubCommandHandler(mockRepo.Object);
        var command = new CreateClubCommand("", "Description");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(command));
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Club>(), default), Times.Never);
    }

    [Fact]
    public async Task CreateClub_WithNameLongerThan50Chars_ThrowsException()
    {
        var mockRepo = new Mock<IClubRepository>();
        var handler = new CreateClubCommandHandler(mockRepo.Object);
        var command = new CreateClubCommand(new string('A', 51), "Description");

        await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(command));
    }
}

public class GetClubsQueryTests
{
    [Fact]
    public async Task GetClubs_WhenEmpty_ReturnsEmptyList()
    {
        // Arrange
        var mockRepo = new Mock<IClubRepository>();
        mockRepo.Setup(r => r.GetAllAsync(default))
                .ReturnsAsync(Enumerable.Empty<Club>());
        var handler = new GetClubsQueryHandler(mockRepo.Object);

        // Act
        var result = await handler.HandleAsync(new GetClubsQuery());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
