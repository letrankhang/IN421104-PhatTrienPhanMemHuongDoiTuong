# Starter Ch02 — Refactoring EduHub với SOLID và Design Patterns

Điểm xuất phát của bài tập Ch02 (xem `de-bai.html` và `rubric.html`). Dự án build được ngay, nhưng `ClubManager` cố tình vi phạm SOLID.

## Việc cần làm

Các đoạn `// TODO` nằm ở:

- `Ch02.Refactor/ClubManager.cs`: danh sách TODO 1 đến 5 ở đầu file, TODO Factory/Observer/Strategy/Decorator ngay tại method liên quan.
- `Ch02.Refactor/Domain/Club.cs`, `Domain/Event.cs`: hoàn thiện `Create`, `HasRoom`, `Register`, `CheckIn`.
- `Ch02.Refactor.Tests/DomainTests.cs`: bỏ `Skip` và viết thân test theo mẫu Arrange-Act-Assert.

## Lệnh

```bash
dotnet build Ch02.Refactor.sln
dotnet test Ch02.Refactor.sln
dotnet run --project Ch02.Refactor
```

## Nộp bài

Làm trên nhánh `feature/solid-refactor`, commit theo Conventional Commits, rebase lên `main` rồi mở Pull Request (mô tả: what, why, how to test).
