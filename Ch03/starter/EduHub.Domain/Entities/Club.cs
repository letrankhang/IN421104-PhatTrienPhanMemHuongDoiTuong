namespace EduHub.Domain.Entities;

/// <summary>Câu lạc bộ — entity của lớp Domain, không phụ thuộc framework nào.</summary>
public class Club
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Club() { }

    public static Club Create(string name, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return new Club { Name = name, Description = description };
    }
}
