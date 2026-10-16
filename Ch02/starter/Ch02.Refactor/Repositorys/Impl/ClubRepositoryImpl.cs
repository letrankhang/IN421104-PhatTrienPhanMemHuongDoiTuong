using EduHub.Ch02.Domain;
using EduHub.Ch02.Repositorys;

namespace EduHub.Ch02.Repositorys.Impl;

public class ClubRepositoryImpl : IClubRepository
{
    private readonly List<Club> _clubs = new();
    public void Add(Club club)
    {
        _clubs.Add(club);
    }
    public Club? GetById(Guid clubId)
    {
        return _clubs.FirstOrDefault(c => c.Id == clubId);
    }
    public IReadOnlyList<Club> GetAll()
    {
        return _clubs.AsReadOnly();
    }
}