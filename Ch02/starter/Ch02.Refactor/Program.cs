using EduHub.Ch02.Enum;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Logging.Impl;
using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Repositorys.Impl;
using EduHub.Ch02.Services;
using EduHub.Ch02.Services.Impl;

IAppLogger logger = new ConsoleLoggerImpl();
INotificationServiceFactory notificationFactory =
    new NotificationServiceFactoryImpl();

IClubRepository clubRepository = new LoggingClubRepository(
    new ClubRepositoryImpl(),
    logger);
IMemberRepository memberRepository = new InMemoryMemberRepositoryImpl();
IEventRepository eventRepository = new InMemoryEventRepositoryImpl();

IClubService clubService = new ClubServiceImpl(
    clubRepository,
    notificationFactory,
    logger,
    memberRepository);
IEventService eventService = new EventServiceImpl(
    clubRepository,
    eventRepository,
    memberRepository);
IReportService reportService = new ReportServiceImpl(
    clubRepository,
    memberRepository,
    eventRepository);

var notifier = notificationFactory.Create(NotificationChannel.Email);
eventService.Subscribe(new EmailNotifierImpl(memberRepository, notifier));
eventService.Subscribe(new LogNotifierImpl(logger));

Console.WriteLine("== Tạo CLB và thêm thành viên ==");
var club = clubService.CreateClub(
    "Tech Club",
    "Câu lạc bộ công nghệ",
    "president@gmail.com");
var member = clubService.AddMembertoClub(
    club.Id,
    "Nguyễn Văn A",
    "a@student.dthu.edu.vn");

Console.WriteLine("\n== Tạo sự kiện và đăng ký ==");
var eventItem = eventService.CreateEvent(
    club.Id,
    "Workshop SOLID",
    new DateTime(2026, 11, 20));
eventService.Register(eventItem.Id, member.Id);
eventService.CheckIn(eventItem.Id, member.Id);

Console.WriteLine("\n== Báo cáo Console ==");
Console.Write(
    reportService.Generate(new ConsoleReportStrategy()));

Console.WriteLine("== Báo cáo CSV ==");
Console.Write(
    reportService.Generate(new CsvReportStrategy()));

Console.WriteLine("== Kiểm tra Factory ==");
foreach (var channel in Enum.GetValues<NotificationChannel>())
{
    notificationFactory
        .Create(channel)
        .Send(member.Email, $"Thử kênh {channel}");
}
