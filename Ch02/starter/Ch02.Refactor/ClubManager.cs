// ═══════════════════════════════════════════════════════════════════════════
//  ClubManager là "God class" cố tình viết sai. Nhiệm vụ của bạn: refactor.
//
//  TODO 1 (Nhiệm vụ 1) Ghi lại ≥ 4 vi phạm SOLID vào docs/solid-analysis.md
//         (dòng nào, nguyên tắc nào, hậu quả gì). Các chú thích "SOLID violation"
//         bên dưới chỉ là gợi ý, hãy tự tìm thêm.
//  TODO 2 (Nhiệm vụ 2) Tách thành ≥ 4 class/interface: ClubService, IClubRepository,
//         EventService, ReportService, IAppLogger, INotificationService.
//         Mọi phụ thuộc đi qua interface, nhận bằng constructor (SRP, DIP, ISP).
//  TODO 3 (Nhiệm vụ 3) Áp dụng 4 pattern:
//         - Factory   : tạo INotificationService theo NotificationChannel (thay chuỗi if/else)
//         - Decorator : LoggingClubRepository bọc IClubRepository để ghi log
//         - Strategy  : IReportStrategy (Console, CSV) thay cho PrintClubReport
//         - Observer  : IEventObserver, khi tạo sự kiện thì báo cho EmailNotifier, LogNotifier
//  TODO 4 (Nhiệm vụ 4) Hoàn thiện Domain/*.cs và viết unit test AAA trong Ch02.Refactor.Tests.
//  TODO 5 (Nhiệm vụ 5) Làm trên nhánh feature/solid-refactor, commit theo Conventional
//         Commits, rebase lên main trước khi mở Pull Request.
// ═══════════════════════════════════════════════════════════════════════════

using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

/// <summary>
/// God class: quản lý CLB, thành viên, sự kiện, gửi thông báo và ghi log trong một class.
/// Vi phạm: SRP, OCP, DIP.
/// </summary>
public class ClubManager
{
    private readonly List<Club> _clubs = new();
    private readonly List<Member> _members = new();
    private readonly string _logFilePath = "club-log.txt";

    // SOLID violation: SRP — method này làm 3 việc: validate, lưu, gửi thông báo
    public void CreateClub(string name, string description, string presidentEmail)
    {
        // Validate
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name is required");
        if (name.Length > 100)
            throw new ArgumentException("Club name too long");

        // Save
        var club = new Club { Id = Guid.NewGuid(), Name = name, Description = description };
        _clubs.Add(club);

        // Log — SOLID violation: DIP — ghi file trực tiếp, không qua abstraction
        File.AppendAllText(_logFilePath, $"[{DateTime.Now}] Club created: {name}\n");

        // Notify — SOLID violation: OCP — muốn thêm SMS phải sửa method này
        // TODO (Factory): thay bằng INotificationServiceFactory.Create(NotificationChannel...)
        if (presidentEmail.Contains("@gmail.com"))
        {
            Console.WriteLine($"Sending Gmail notification to {presidentEmail}");
        }
        else if (presidentEmail.Contains("@yahoo.com"))
        {
            Console.WriteLine($"Sending Yahoo notification to {presidentEmail}");
        }
        else
        {
            Console.WriteLine($"Sending generic email to {presidentEmail}");
        }
    }

    // SOLID violation: SRP — method này làm cả nghiệp vụ lẫn gửi thông báo
    public void AddMemberToClub(Guid clubId, string memberName, string email)
    {
        var club = _clubs.FirstOrDefault(c => c.Id == clubId)
            ?? throw new InvalidOperationException("Club not found");

        if (_members.Count(m => m.ClubId == clubId) >= 50)
            throw new InvalidOperationException("Club is full (max 50 members)");

        var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
        _members.Add(member);

        // DIP violation: ghi file trực tiếp
        File.AppendAllText(_logFilePath, $"[{DateTime.Now}] Member {memberName} joined {club.Name}\n");

        // OCP violation: thêm kênh thông báo phải sửa method này
        Console.WriteLine($"Welcome email sent to {email}");
        Console.WriteLine($"Slack notification sent: {memberName} joined {club.Name}");
    }

    // SOLID violation: SRP + OCP — tạo sự kiện nhưng cũng tự gửi thông báo cho từng thành viên
    // TODO (Observer): bỏ đoạn gửi thông báo ở đây; EventService chỉ gọi OnEventCreated cho các observer đã đăng ký.
    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubs.FirstOrDefault(c => c.Id == clubId)
            ?? throw new InvalidOperationException("Club not found");

        var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        File.AppendAllText(_logFilePath, $"[{DateTime.Now}] Event created: {title}\n");

        foreach (var m in _members.Where(m => m.ClubId == clubId))
            Console.WriteLine($"Email to {m.Email}: new event '{title}' at {club.Name}");
        return ev;
    }

    // SOLID violation: SRP — báo cáo trộn với nghiệp vụ; OCP — muốn xuất CSV phải sửa method này
    // TODO (Strategy): ReportService.Generate(IReportStrategy) với ConsoleReportStrategy và CsvReportStrategy.
    public void PrintClubReport()
    {
        foreach (var club in _clubs)
        {
            var memberCount = _members.Count(m => m.ClubId == club.Id);
            Console.WriteLine($"Club: {club.Name} | Members: {memberCount}");

            // DIP violation: ghi file ngay trong lúc in báo cáo
            File.AppendAllText(_logFilePath, $"Report generated for {club.Name}\n");
        }
    }

    // TODO (Decorator): sau khi có IClubRepository, viết LoggingClubRepository bọc nó để ghi log mỗi lần truy cập.
    public List<Club> GetAllClubs() => _clubs;
}
