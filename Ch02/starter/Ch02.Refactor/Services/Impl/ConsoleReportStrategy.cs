using System.Text;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class ConsoleReportStrategy : IReportStrategy
{
    public string Render(IReadOnlyList<ClubReportRow> rows)
    {
        var output = new StringBuilder();

        foreach (var row in rows)
        {
            output.AppendLine(
                $"Club: {row.ClubName} | Members: {row.Members} | " +
                $"Events: {row.Events} | Registered: {row.Registered} | " +
                $"Checked-in: {row.CheckedIn}");
        }

        return output.ToString();
    }
}
