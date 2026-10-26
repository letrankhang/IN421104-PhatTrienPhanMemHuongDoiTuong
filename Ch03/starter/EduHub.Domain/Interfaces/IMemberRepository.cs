using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync(CancellationToken ct = default);
    Task<Member?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Member?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task AddAsync(Member member, CancellationToken ct = default);
    Task UpdateAsync(Member member, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
