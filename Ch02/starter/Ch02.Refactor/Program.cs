using EduHub.Ch02;

// ═══════════════════════════════════════════════════════════════
//  EduHub Ch02 — Refactoring với SOLID và Design Patterns
//  ClubManager.cs chứa code vi phạm SOLID (đọc các TODO ở đầu file đó).
// ═══════════════════════════════════════════════════════════════

// Demo code cũ (vi phạm SOLID) — chạy được nhưng thiết kế rất xấu:
var manager = new ClubManager();
manager.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
manager.AddMemberToClub(manager.GetAllClubs()[0].Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");
manager.CreateEvent(manager.GetAllClubs()[0].Id, "Workshop SOLID", new DateTime(2026, 11, 20));
manager.PrintClubReport();

Console.WriteLine("\n--- TODO: refactor code trên (xem checklist trong de-bai.html) ---");
Console.WriteLine("1. Phân tích vi phạm SOLID  ->  docs/solid-analysis.md");
Console.WriteLine("2. Tách ClubManager thành các class nhỏ, phụ thuộc qua interface (SRP, OCP, DIP)");
Console.WriteLine("3. Áp dụng Factory, Decorator, Strategy, Observer");
Console.WriteLine("4. Hoàn thiện Domain và viết unit test AAA (dotnet test)");
Console.WriteLine("5. Nhánh feature/solid-refactor, Conventional Commits, rebase, Pull Request");
