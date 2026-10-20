using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Domain;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class EventServiceImpl : IEventService
{
    private readonly IClubRepository _clubs;
    private readonly IEventRepository _events;
    private readonly IMemberRepository _members;
    private readonly List<IEventObserver> _observers = new();

    public EventServiceImpl(
        IClubRepository clubs,
        IEventRepository events,
        IMemberRepository members)
    {
        _clubs = clubs;
        _events = events;
        _members = members;
    }

    public void Subscribe(IEventObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Add(observer);
    }

    public void Unsubscribe(IEventObserver observer) => _observers.Remove(observer);

    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Club không tồn tại");
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Tiêu đề sự kiện không được để trống.",
                nameof(title));
        }

        var ev = new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title,
            Date = date
        };
        _events.Add(ev);
        foreach (var observer in _observers)
        {
            observer.OnEventCreated(ev, club);
        }
        return ev;
    }

    public void Register(Guid eventId, Guid memberId)
    {
        var ev = GetEvent(eventId);
        var member = _members.GetById(memberId)
            ?? throw new InvalidOperationException("Thành viên không tồn tại");

        if (member.ClubId != ev.ClubId)
        {
            throw new InvalidOperationException("Thành viên không thuộc câu lạc bộ của sự kiện");
        }

        if (!ev.Register(memberId))
        {
            throw new InvalidOperationException(
                "Thành viên đã đăng ký sự kiện này");
        }
    }

    public void CheckIn(Guid eventId, Guid memberId)
    {
        var ev = GetEvent(eventId);

        var member = _members.GetById(memberId)
            ?? throw new InvalidOperationException("Thành viên không tồn tại");

        if (member.ClubId != ev.ClubId)
        {
            throw new InvalidOperationException(
                "Thành viên không thuộc câu lạc bộ của sự kiện");
        }

        ev.CheckIn(memberId);
    }
    private Event GetEvent(Guid id) => _events.GetById(id)
        ?? throw new InvalidOperationException("Sự kiện không tồn tại");
}