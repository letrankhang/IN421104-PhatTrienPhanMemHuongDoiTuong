namespace EduHub.Ch02.Domain;

/// <summary>Thành viên của một CLB.</summary>
public class Member
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public Guid ClubId { get; set; }
}
