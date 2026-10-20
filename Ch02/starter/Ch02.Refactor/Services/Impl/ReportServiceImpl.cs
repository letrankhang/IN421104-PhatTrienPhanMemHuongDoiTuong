using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class ReportServiceImpl : IReportService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly IEventRepository _events;

    public ReportServiceImpl(
        IClubRepository clubs,
        IMemberRepository members,
        IEventRepository events)
    {
        _clubs = clubs;
        _members = members;
        _events = events;
    }

    public string Generate(IReportStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);

        var rows = _clubs.GetAll()
            .Select(club =>
            {
                var events = _events.GetByClubId(club.Id);
                return new ClubReportRow(
                    club.Name,
                    _members.GetByClubId(club.Id).Count,
                    events.Count,
                    events.Sum(eventItem => eventItem.RegisteredMemberIds.Count),
                    events.Sum(eventItem => eventItem.CheckedInMemberIds.Count));
            })
            .ToList();

        return strategy.Render(rows);
    }
}
