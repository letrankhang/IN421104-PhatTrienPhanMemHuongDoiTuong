const string Separator = "========================================";
const int EventCapacity = 2;

static void Step (int number, string title) =>
    Console.WriteLine ($"\n{Separator}\nBước {number}: {title}\n{Separator}");

Step (1, "Tạo câu lạc bộ");
var club = Club.Create (
    "Tech Club",
    "Câu lạc bộ công nghệ: workshop, hackathon, chia sẻ kiến thức.");
Console.WriteLine ($"CLB: {club.Name} — {club.Description}");

Step (2, "Thêm thành viên");
var members = new[]
{
    Member.Create("Nguyễn Văn An", "an.nv@student.dthu.edu.vn", "DH24001"),
    Member.Create("Trần Thị Bình", "binh.tt@student.dthu.edu.vn", "DH24002"),
    Member.Create("Lê Hoàng Cường", "cuong.lh@student.dthu.edu.vn", "DH24003"),
};

foreach( var member in members )
{
    club.AddMember (member);
    Console.WriteLine ($"+ {member} <{member.Email}>");
}

Console.WriteLine ($"Tổng thành viên: {club.MemberCount ()}");

Step (3, "Tạo sự kiện");
var workshop = Event.Create (
    club.Id,
    "Workshop AI",
    DateTime.Today.AddDays (7).AddHours (14),
    EventCapacity,
    isOnline: false);
Console.WriteLine (
    $"Sự kiện: {workshop.Title} — {workshop.Date:dd/MM/yyyy HH:mm} — " +
    $"sức chứa {workshop.Capacity}");

Step (4, "Đăng ký tham dự");
var registrations = new List<Registration> ();

foreach( var member in members )
{
    if( !workshop.HasCapacity (registrations.Count) )
    {
        Console.WriteLine ($"! Hết chỗ, không nhận {member}");
        continue;
    }

    var registration = Registration.Create (member.Id, workshop.Id);
    registrations.Add (registration);
    Console.WriteLine ($"{member} đăng ký → {registration.Status}");
}

Step (5, "Xác nhận đăng ký");
foreach( var registration in registrations )
{
    registration.Confirm ();
    Console.WriteLine (
        $"Phiếu {registration.Id.ToString ()[..8]} → {registration.Status}");
}

Step (6, "Check-in");
var checkIns = new List<CheckIn>
{
    CheckIn.Create(registrations[0], CheckInMethod.QrCode),
};
Console.WriteLine (checkIns[0].Summary ());

Step (7, "Báo cáo tham dự");
Console.WriteLine ($"Số đăng ký: {registrations.Count}/{workshop.Capacity}");
Console.WriteLine ($"Số check-in: {checkIns.Count}");

var memberById = members.ToDictionary (member => member.Id);
foreach( var registration in registrations )
{
    var attended = checkIns.Any (checkIn =>
        checkIn.RegistrationId == registration.Id);
    var member = memberById[registration.MemberId];
    Console.WriteLine (
        $"- {member.Name}: {(attended ? "đã check-in" : "chưa check-in")}");
}

Step (8, "Kiểm tra quy tắc nghiệp vụ");
var cancelledRegistration = Registration.Create (members[2].Id, workshop.Id);
cancelledRegistration.Cancel ();
Console.WriteLine (
    $"Phiếu {cancelledRegistration.Id.ToString ()[..8]} → " +
    $"{cancelledRegistration.Status}");

try
{
    cancelledRegistration.Confirm ();
}
catch( InvalidOperationException exception )
{
    Console.WriteLine ($"Đúng như mong đợi: {exception.Message}");
}

try
{
    CheckIn.Create (cancelledRegistration);
}
catch( InvalidOperationException exception )
{
    Console.WriteLine ($"Đúng như mong đợi: {exception.Message}");
}
