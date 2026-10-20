using EduHub.Ch02.Domain;
using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class EmailNotifierImpl : IEventObserver
{
    private readonly IMemberRepository _member;
    private readonly INotificationService _notifi;

    public EmailNotifierImpl(
        IMemberRepository member,
        INotificationService notifi)
    {
        _member = member;
        _notifi = notifi;
    }
    public void OnEventCreated(Event ev, Club club)
    {
        foreach (var member in _member.GetByClubId(club.Id))
        {
            _notifi.Send(
                member.Email,
                $"New event '{ev.Title}' at {club.Name} on {ev.Date:dd/MM/yyyy}");
        }
    }
}