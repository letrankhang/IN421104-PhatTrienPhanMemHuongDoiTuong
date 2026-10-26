using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record CreateClubCommand(string Name, string Description);

public class CreateClubCommandHandler
{
    private const int MaxNameLength = 50;
    private readonly IClubRepository _repository;

    public CreateClubCommandHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public Task<Club> HandleAsync(CreateClubCommand command, CancellationToken ct = default)
    {
        // TODO 4a: throw ArgumentException nếu Name rỗng hoặc toàn khoảng trắng
        // TODO 4b: throw ArgumentException nếu Name dài hơn MaxNameLength (50) ký tự
        // TODO 4c: tạo club bằng Club.Create(...), lưu bằng _repository.AddAsync(club, ct)
        // TODO 4d: trả về club vừa tạo
        throw new NotImplementedException();
    }
}
