using EduHub.Ch02.Domain;
using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public class ClubServiceImpl : IClubService
{
    private readonly IClubRepository _clubRepository;
    public ClubServiceImpl(IClubRepository cluRepo)
    {
        _clubRepository = cluRepo;
    }
    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        _clubRepository.Add(club);
        return club;
    }
    public Member AddMembertoClub(Guid clubId, string name, string email)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            ClubId = clubId
        };
        return member;
    }

    public IReadOnlyList<Club> GetAllClubs()
    {
        return _clubRepository.GetAll();
    }
}