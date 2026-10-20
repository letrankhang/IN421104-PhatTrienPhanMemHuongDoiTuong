using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public interface IEventService
{
    void Subscribe(IEventObserver observer);
    void Unsubscribe(IEventObserver observer);
    Event CreateEvent(Guid clubId, string title, DateTime date);
    void Register(Guid eventId, Guid memberId);
    void CheckIn(Guid eventId, Guid memberId);
}