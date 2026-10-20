using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

public class DomainTests
{
    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        const string name = "CLB Lập trình";
        const string description = "Cộng đồng lập trình";

        var club = Club.Create(name, description);

        Assert.NotEqual(Guid.Empty, club.Id);
        Assert.Equal(name, club.Name);
        Assert.Equal(description, club.Description);
    }

    [Fact]
    public void Club_Create_WithInvalidName_ThrowsArgumentException()
    {
        var longName = new string('A', Club.MaxNameLength + 1);

        Assert.Throws<ArgumentException>(
            () => Club.Create("", "Mô tả"));
        Assert.Throws<ArgumentException>(
            () => Club.Create(longName, "Mô tả"));
    }

    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        var club = Club.Create("CLB", "Mô tả");

        Assert.True(club.HasRoom(Club.MaxMembers - 1));
        Assert.False(club.HasRoom(Club.MaxMembers));
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        var eventItem = CreateEvent();
        var memberId = Guid.NewGuid();

        var result = eventItem.Register(memberId);

        Assert.True(result);
        Assert.Contains(memberId, eventItem.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        var eventItem = CreateEvent();
        var memberId = Guid.NewGuid();

        eventItem.Register(memberId);
        var secondResult = eventItem.Register(memberId);

        Assert.False(secondResult);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        var eventItem = CreateEvent();

        var act = () => eventItem.CheckIn(Guid.NewGuid());

        Assert.Throws<InvalidOperationException>(act);
    }

    private static Event CreateEvent()
    {
        return new Event
        {
            Id = Guid.NewGuid(),
            ClubId = Guid.NewGuid(),
            Title = "Workshop",
            Date = DateTime.UtcNow
        };
    }
}
