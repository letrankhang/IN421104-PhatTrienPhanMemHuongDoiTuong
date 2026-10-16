# Phân tích vi phạm SOLID trong `ClubManager`

## 1. Phạm vi phân tích

Tài liệu này phân tích mã nguồn starter thực tế tại [`ClubManager.cs`](../starter/Ch02.Refactor/ClubManager.cs). Đây là lớp được bài tập cố tình viết theo kiểu **God class** để làm cơ sở refactor.

Lớp hiện tại đồng thời:

- quản lý danh sách câu lạc bộ và thành viên;
- thực hiện kiểm tra dữ liệu đầu vào;
- lưu dữ liệu;
- ghi log trực tiếp ra file;
- gửi thông báo;
- tạo sự kiện;
- sinh báo cáo.

Vì vậy, một thay đổi ở bất kỳ chức năng nào cũng có thể buộc phải sửa `ClubManager`.

## 2. Tổng hợp các vi phạm

| STT | Nguyên tắc | Vị trí minh chứng | Vi phạm chính | Hậu quả |
|---|---|---|---|---|
| 1 | SRP | Dòng 34-64 | `CreateClub` vừa validate, tạo/lưu CLB, ghi log và gửi thông báo | Lớp có nhiều lý do để thay đổi, khó kiểm thử |
| 2 | SRP | Dòng 66-84 | `AddMemberToClub` vừa xử lý nghiệp vụ thành viên, ghi log và gửi nhiều thông báo | Nghiệp vụ bị trộn với hạ tầng và giao tiếp |
| 3 | SRP | Dòng 86-99 | `CreateEvent` vừa tạo sự kiện, ghi log, duyệt thành viên và gửi email | Sửa cách thông báo có thể ảnh hưởng nghiệp vụ tạo sự kiện |
| 4 | SRP | Dòng 101-113 | `PrintClubReport` vừa tính dữ liệu báo cáo, xuất Console và ghi log | Báo cáo không thể tái sử dụng cho CSV/API một cách độc lập |
| 5 | OCP | Dòng 50-63 | Chuỗi `if/else` quyết định Gmail, Yahoo hoặc email mặc định | Thêm SMS/Slack phải sửa mã đang chạy |
| 6 | OCP | Dòng 81-83 | Gắn cứng Email và Slack trong `AddMemberToClub` | Kênh thông báo mới làm tăng điều kiện và nguy cơ hồi quy |
| 7 | OCP | Dòng 96-97 | `CreateEvent` gắn cứng cách gửi email cho từng thành viên | Không thể thêm SMS, push notification hoặc queue mà không sửa service |
| 8 | OCP | Dòng 101-108 | Báo cáo gắn cứng định dạng Console | Thêm CSV/JSON/HTML phải sửa `PrintClubReport` |
| 9 | DIP | Dòng 30-32, 47-48 | Lớp nghiệp vụ tự quản lý bộ nhớ và tự ghi file bằng `File.AppendAllText` | Không thay được database/logger, test bị phụ thuộc file thật |
| 10 | DIP | Dòng 50-63, 81-83, 96-97 | Lớp cấp cao phụ thuộc trực tiếp vào `Console.WriteLine` | Khó thay đổi adapter gửi thông báo và khó mock khi test |
| 11 | DIP | Dòng 69-76, 90-93, 105-108, 116 | `ClubManager` tự lưu trữ và truy vấn bằng `List<T>` | Business logic bị khóa vào cách lưu trữ hiện tại |
| 12 | Encapsulation/thiết kế liên quan SRP | Dòng 116 | Trả trực tiếp `_clubs` qua `GetAllClubs` | Client có thể phụ thuộc vào cấu trúc lưu trữ nội bộ; sau này đổi repository sẽ khó |

> **Lưu ý:** Trong file hiện tại chưa có quan hệ kế thừa hoặc interface đa chức năng nên chưa có minh chứng trực tiếp cho LSP và ISP. Không nên kết luận hai nguyên tắc này bị vi phạm chỉ từ `ClubManager`. Tuy nhiên, khi refactor, ISP và DIP cần được áp dụng khi thiết kế các interface mới.

## 3. Minh chứng chi tiết

### 3.1. Vi phạm SRP

#### Minh chứng 1: `CreateClub`

Đoạn mã tại dòng 38-48 thực hiện nhiều trách nhiệm:

```csharp
if (string.IsNullOrWhiteSpace(name))
    throw new ArgumentException("Club name is required");
if (name.Length > 100)
    throw new ArgumentException("Club name too long");

var club = new Club { Id = Guid.NewGuid(), Name = name, Description = description };
_clubs.Add(club);

File.AppendAllText(_logFilePath, $"[{DateTime.Now}] Club created: {name}\n");
```

Sau đó, dòng 52-63 lại quyết định kênh thông báo. Như vậy `CreateClub` có ít nhất bốn lý do để thay đổi:

1. luật kiểm tra tên CLB thay đổi;
2. cách lưu CLB thay đổi;
3. cách ghi log thay đổi;
4. kênh hoặc nội dung thông báo thay đổi.

#### Minh chứng 2: `CreateEvent`

```csharp
var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
File.AppendAllText(_logFilePath, $"[{DateTime.Now}] Event created: {title}\n");

foreach (var m in _members.Where(m => m.ClubId == clubId))
    Console.WriteLine($"Email to {m.Email}: new event '{title}' at {club.Name}");
```

Phương thức vừa tạo domain object, vừa ghi log, vừa duyệt thành viên, vừa gửi email. Việc thay đổi cơ chế gửi thông báo không nên làm thay đổi nghiệp vụ tạo sự kiện.

### 3.2. Vi phạm OCP

#### Minh chứng 3: chuỗi điều kiện chọn thông báo

```csharp
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
```

Để hỗ trợ SMS, Slack hoặc push notification, phải mở và sửa `CreateClub`. Đây là dấu hiệu mã chưa đóng đối với thay đổi nhưng chưa mở đủ để mở rộng.

`PrintClubReport` cũng vi phạm OCP vì chỉ biết xuất Console:

```csharp
Console.WriteLine($"Club: {club.Name} | Members: {memberCount}");
```

Nếu cần CSV, JSON hoặc HTML, phương thức hiện tại phải chứa thêm `if/else` hoặc `switch`.

### 3.3. Vi phạm DIP

#### Minh chứng 4: phụ thuộc trực tiếp vào file system

```csharp
private readonly string _logFilePath = "club-log.txt";

File.AppendAllText(
    _logFilePath,
    $"[{DateTime.Now}] Club created: {name}\n");
```

`ClubManager` là module cấp cao chứa nghiệp vụ nhưng lại tự quyết định chi tiết cấp thấp là file `club-log.txt`. Khi test, mã sẽ ghi file thật; khi triển khai cần database, structured logging hoặc Azure Application Insights thì phải sửa lớp nghiệp vụ.

#### Minh chứng 5: phụ thuộc trực tiếp vào Console

```csharp
Console.WriteLine($"Welcome email sent to {email}");
Console.WriteLine($"Slack notification sent: {memberName} joined {club.Name}");
```

Nghiệp vụ không nên biết thông báo được gửi bằng Console, SMTP, Slack hay message broker nào. Các chi tiết này phải được cung cấp qua abstraction.

#### Minh chứng 6: phụ thuộc vào cấu trúc lưu trữ cụ thể

```csharp
private readonly List<Club> _clubs = new();
private readonly List<Member> _members = new();

public List<Club> GetAllClubs() => _clubs;
```

Các thao tác nghiệp vụ truy vấn trực tiếp `List<T>`. Vì vậy, việc chuyển sang repository, Entity Framework hoặc database sẽ kéo theo thay đổi ở `ClubManager`.

## 4. Hướng giải quyết đề xuất

### 4.1. Tách trách nhiệm theo SRP

Nên chia `ClubManager` thành các thành phần sau:

| Thành phần | Trách nhiệm |
|---|---|
| `ClubService` | Điều phối use case tạo CLB và thêm thành viên |
| `EventService` | Tạo sự kiện và phát sự kiện domain |
| `ReportService` | Lấy dữ liệu cần báo cáo và giao cho strategy xuất kết quả |
| `IClubRepository` | Abstraction cho lưu/truy vấn CLB |
| `IMemberRepository` | Abstraction cho lưu/truy vấn thành viên |
| `IAppLogger` | Ghi log |
| `INotificationService` | Gửi thông báo |
| `IReportStrategy` | Một định dạng báo cáo |

Luật tạo CLB nên chuyển vào `Club.Create`, còn luật sức chứa nên chuyển vào `Club.HasRoom`. Đây cũng là yêu cầu đã ghi trong [Domain/Club.cs](../starter/Ch02.Refactor/Domain/Club.cs).

### 4.2. Áp dụng DIP bằng constructor injection

Ví dụ hướng thiết kế:

```csharp
public sealed class ClubService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly IAppLogger _logger;
    private readonly INotificationServiceFactory _notifications;

    public ClubService(
        IClubRepository clubs,
        IMemberRepository members,
        IAppLogger logger,
        INotificationServiceFactory notifications)
    {
        _clubs = clubs;
        _members = members;
        _logger = logger;
        _notifications = notifications;
    }
}
```

`ClubService` chỉ phụ thuộc interface. Khi unit test, có thể truyền fake repository, fake logger và fake notification service thay vì ghi file hoặc in Console thật.

### 4.3. Áp dụng Factory cho thông báo

Thay chuỗi `if/else` bằng:

```csharp
public enum NotificationChannel
{
    Email,
    Sms,
    Slack
}

public interface INotificationService
{
    void Send(string recipient, string message);
}

public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}
```

Mỗi kênh (`EmailNotificationService`, `SmsNotificationService`, `SlackNotificationService`) triển khai interface. Thêm kênh mới chỉ cần thêm implementation và đăng ký factory, không sửa nghiệp vụ tạo CLB.

### 4.4. Áp dụng Decorator cho logging repository

Repository thật chỉ tập trung lưu dữ liệu:

```csharp
public interface IClubRepository
{
    void Add(Club club);
    Club? GetById(Guid id);
    IReadOnlyList<Club> GetAll();
}
```

Logging được bọc bên ngoài:

```csharp
public sealed class LoggingClubRepository : IClubRepository
{
    private readonly IClubRepository _inner;
    private readonly IAppLogger _logger;

    public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public void Add(Club club)
    {
        _inner.Add(club);
        _logger.Info($"Club created: {club.Name}");
    }

    // Các phương thức còn lại ủy quyền cho _inner và ghi log khi cần.
}
```

Nhờ đó, repository không bị trộn với logic log và `ClubService` không gọi `File.AppendAllText`.

### 4.5. Áp dụng Strategy cho báo cáo

```csharp
public interface IReportStrategy
{
    string Generate(IReadOnlyList<ClubReportRow> rows);
}

public sealed class ConsoleReportStrategy : IReportStrategy { }
public sealed class CsvReportStrategy : IReportStrategy { }
```

`ReportService` chỉ lấy dữ liệu và gọi strategy. Khi thêm JSON hoặc HTML, tạo strategy mới thay vì sửa `PrintClubReport`.

### 4.6. Áp dụng Observer cho sự kiện

`EventService` chỉ tạo sự kiện và phát thông báo `OnEventCreated`. Các observer độc lập xử lý phản ứng:

```csharp
public interface IEventObserver
{
    void OnEventCreated(Event clubEvent, Club club);
}

public sealed class EmailEventObserver : IEventObserver { }
public sealed class LogEventObserver : IEventObserver { }
```

Như vậy, `EventService` không cần biết có bao nhiêu người nhận, gửi bằng email hay ghi log. Thêm observer mới không làm thay đổi logic tạo sự kiện.

## 5. Lợi ích sau refactor

- Mỗi lớp có một trách nhiệm rõ ràng hơn, giảm tác động lan truyền khi thay đổi.
- Có thể thay file bằng database hoặc logger khác mà không sửa nghiệp vụ.
- Có thể thêm SMS, Slack, CSV hoặc JSON bằng implementation mới.
- Unit test không cần phụ thuộc file system và Console.
- Các pattern Factory, Decorator, Strategy và Observer đáp ứng đúng yêu cầu bài tập.
- Không khẳng định sai LSP/ISP khi mã hiện tại chưa có bằng chứng trực tiếp; ISP sẽ được bảo đảm bằng các interface nhỏ, chuyên biệt trong thiết kế mới.

## 6. Kế hoạch triển khai và kiểm thử

1. Hoàn thiện `Club.Create`, `Club.HasRoom`, `Event.Register` và `Event.CheckIn`.
2. Tạo interface repository, logger, notification, report strategy và event observer.
3. Tách lần lượt `ClubService`, `EventService`, `ReportService`.
4. Đăng ký dependency bằng constructor injection.
5. Thêm unit test theo AAA cho tạo CLB, giới hạn thành viên, tạo sự kiện và báo cáo.
6. Chạy:

```bash
dotnet build Ch02.Refactor.sln
dotnet test Ch02.Refactor.sln
dotnet run --project Ch02.Refactor
```

