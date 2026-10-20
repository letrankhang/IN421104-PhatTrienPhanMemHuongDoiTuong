using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositorys;

public interface IClubRepository
{
    void Add(Club club);
    Club? GetById(Guid clubId);
    IReadOnlyList<Club> GetAll();
}