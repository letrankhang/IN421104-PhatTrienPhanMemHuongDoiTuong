using EduHub.Ch02;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // Test mẫu đã viết sẵn: chạy được ngay trên ClubManager hiện tại.
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var manager = new ClubManager();

        // Act
        var act = () => manager.CreateClub("", "mô tả", "p@gmail.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // TODO (Nhiệm vụ 4): bỏ Skip và viết thân test cho từng trường hợp dưới đây.
    // Cần ≥ 6 test AAA cho lớp Domain (Club, Event), trong đó có cả trường hợp lỗi.

    [Fact(Skip = "TODO: viết test Club.Create với tên hợp lệ")]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(Skip = "TODO: viết test Club.Create với tên rỗng hoặc dài hơn 100 ký tự")]
    public void Club_Create_WithInvalidName_ThrowsArgumentException()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(Skip = "TODO: viết test Club.HasRoom ở biên 49 và 50 thành viên")]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(Skip = "TODO: viết test Event.Register thành viên mới")]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(Skip = "TODO: viết test Event.Register cùng một thành viên hai lần")]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(Skip = "TODO: viết test Event.CheckIn khi chưa đăng ký")]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange

        // Act

        // Assert
    }
}
