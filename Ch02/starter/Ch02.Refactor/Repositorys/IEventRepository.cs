using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositorys;

public interface IEventRepository
{
    IReadOnlyList<Event> GetByClubId(Guid clubId);
    Event? GetById(Guid eventId);
    void Add(Event ev);
}