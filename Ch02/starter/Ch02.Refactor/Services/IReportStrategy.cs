namespace EduHub.Ch02.Services;

public sealed record ClubReportRow(
    string ClubName,
    int Members,
    int Events,
    int Registered,
    int CheckedIn);

public interface IReportStrategy
{
    string Render(IReadOnlyList<ClubReportRow> rows);
}
