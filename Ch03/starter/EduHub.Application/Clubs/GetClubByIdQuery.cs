using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record GetClubByIdQuery(Guid Id);

public class GetClubByIdQueryHandler
{
    private readonly IClubRepository _repository;

    public GetClubByIdQueryHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public Task<Club?> HandleAsync(GetClubByIdQuery query, CancellationToken ct = default)
    {
        // TODO 3: gọi _repository.GetByIdAsync; trả null nếu không có club
        throw new NotImplementedException();
    }
}
