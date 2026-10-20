using System.Text;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class CsvReportStrategy : IReportStrategy
{
    public string Render(IReadOnlyList<ClubReportRow> rows)
    {
        var output = new StringBuilder(
            "Club,Members,Events,Registered,CheckedIn" +
            Environment.NewLine);

        foreach (var row in rows)
        {
            output.AppendLine(
                $"\"{row.ClubName.Replace("\"", "\"\"")}\"," +
                $"{row.Members},{row.Events},{row.Registered},{row.CheckedIn}");
        }

        return output.ToString();
    }
}
