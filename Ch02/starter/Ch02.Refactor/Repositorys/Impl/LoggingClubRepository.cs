using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;

namespace EduHub.Ch02.Repositorys.Impl;

public sealed class LoggingClubRepository : IClubRepository
{
    private readonly IClubRepository _inner;
    private readonly IAppLogger _logger;

    public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public void Add(Club club)
    {
        _logger.Info($"[repo] Add club '{club.Name}'");
        _inner.Add(club);
    }

    public Club? GetById(Guid clubId)
    {
        _logger.Info($"[repo] GetById {clubId}");
        return _inner.GetById(clubId);
    }

    public IReadOnlyList<Club> GetAll()
    {
        _logger.Info("[repo] GetAll clubs");
        return _inner.GetAll();
    }
}
