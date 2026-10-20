using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Repositorys;
using EduHub.Ch02.Services;
using EduHub.Ch02.Enum;

namespace EduHub.Ch02.Services.Impl;

public class ClubServiceImpl : IClubService
{
    private readonly IClubRepository _clubRepository;
    private readonly INotificationServiceFactory _notificationFactory;
    private readonly IAppLogger _logger;
    private readonly IMemberRepository _member;
    public ClubServiceImpl(
        IClubRepository cluRepo,
        INotificationServiceFactory notificationFactory,
        IAppLogger logger,
        IMemberRepository member)
    {
        _clubRepository = cluRepo;
        _notificationFactory = notificationFactory;
        _logger = logger;
        _member = member;
    }
    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        _clubRepository.Add(club);
        _logger.Info($"Club created: {name}");
        _notificationFactory
            .Create(NotificationChannel.Email)
            .Send(presidentEmail, $"Welcome to the club {name}!");
        return club;
    }
    public Member AddMembertoClub(Guid clubId, string memberName, string email)
    {
        var club = _clubRepository.GetById(clubId)
            ?? throw new InvalidOperationException("Club không tồn tại");
        if(!club.HasRoom(_member.GetByClubId(clubId).Count))
            throw new InvalidOperationException($"Club đã đầy (tối đa {Club.MaxMembers} thành viên)");
        var member = new Member
        {
            Id = Guid.NewGuid(),
            Name = memberName,
            Email = email,
            ClubId = clubId
        };
        _member.Add(member);
        _logger.Info($"Member added: {memberName} tham gia thành công {club.Name}");
        _notificationFactory
            .Create(NotificationChannel.Email)
            .Send(email, $"Chào mừng bạn đến với {club.Name}!");
        return member;
    }
}