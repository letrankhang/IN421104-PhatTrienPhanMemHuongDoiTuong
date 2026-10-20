namespace EduHub.Ch02.Domain;

/// <summary>Câu lạc bộ. Quy tắc nghiệp vụ nên nằm ở đây, không nằm trong ClubManager.</summary>
public class Club
{
    public const int MaxMembers = 50;
    public const int MaxNameLength = 100;

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tên câu lạc bộ không được để trống hoặc toàn khoảng trắng.", nameof(name));
        }
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Tên câu lạc bộ không được dài hơn {MaxNameLength} ký tự.", nameof(name));
        }
        return new Club
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description ?? string.Empty
        };
    }

    public bool HasRoom(int currentMemberCount)
    {
        return currentMemberCount < MaxMembers;
    }
}
