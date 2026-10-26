using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

public interface IClubRepository
{
    Task<IEnumerable<Club>> GetAllAsync(CancellationToken ct = default);
    Task<Club?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Club club, CancellationToken ct = default);
    Task UpdateAsync(Club club, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
