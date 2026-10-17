using EduHub.Ch02.Domain;
using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public class ClubServiceImpl : IClubService
{
    private readonly IClubRepository _clubRepository;
    private readonly INotificationService _notifier;
    private readonly IAppLogger _logger;
    private readonly IMemberRepository _member;
    public ClubServiceImpl(IClubRepository cluRepo, INotificationService notifier, IAppLogger logger, IMemberRepository member)
    {
        _clubRepository = cluRepo;
        _notifier = notifier;
        _logger = logger;
        _member = member;
    }
    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        _clubRepository.Add(club);
        _logger.Info($"Club created: {name}");
        _notifier.Send(presidentEmail, $"Welcome to the club {name}!");
        return club;
    }
    public Member AddMembertoClub(Guid clubId, string name, string email)
    {
        var club = _clubRepository.GetById(clubId)
            ?? throw new InvalidOperationException("Club khong tồn tại");
        if(!club.HasRoom(_member.GetByClubId(clubId).Count))
            throw new InvalidOperationException($"Club đã đầy (tối đa {Club.MaxMembers} thành viên)");
        var member = new Member
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            ClubId = clubId
        };
        _member.Add(member);
        _logger.Info($"Member added: {name} tham gia thành công {club.Name}");
        _notifier.Send(email, $"Chào mừng bạn đến với {club.Name}!");
        return member;
    }
}