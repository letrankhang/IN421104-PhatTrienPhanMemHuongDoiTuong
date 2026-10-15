public enum MemberStatus
{
    Active,
    Inactive
}
public class Member
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string StudentCode { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public MemberStatus Status { get; private set; }

    private Member(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
        Status = MemberStatus.Active;
    }

    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được để trống.", nameof(name));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email không được để trống.", nameof(email));
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã sinh viên không được để trống.", nameof(studentCode));

        return new Member(name.Trim(), email.Trim().ToLowerInvariant(), studentCode.Trim());
    }

    public bool IsActive()
    {
        return Status == MemberStatus.Active;
    }

    public void Activate()
    {
        Status = MemberStatus.Active;
    }

    public void Deactivate()
    {
        Status = MemberStatus.Inactive;
    }

    public override string ToString()
    {
        return $"{Name} - {StudentCode}";
    }
}