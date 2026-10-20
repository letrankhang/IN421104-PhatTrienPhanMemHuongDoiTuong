using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositorys.Impl;

public sealed class InMemoryEventRepositoryImpl : IEventRepository
{
    private readonly List<Event> _store = new();

    public IReadOnlyList<Event> GetByClubId(Guid clubId)
    {
        return _store.Where(e => e.ClubId == clubId).ToList().AsReadOnly();
    }

    public Event? GetById(Guid eventId)
    {
        return _store.FirstOrDefault(e => e.Id == eventId);
    }

    public void Add(Event ev)
    {
        _store.Add(ev);
    }
}