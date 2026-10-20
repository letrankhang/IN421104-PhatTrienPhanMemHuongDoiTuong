using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class LogNotifierImpl : IEventObserver
{
    private readonly IAppLogger _logger;

    public LogNotifierImpl(IAppLogger logger)
    {
        _logger = logger;
    }

    public void OnEventCreated(Event ev, Club club)
    {
        _logger.Info(
            $"Event created: {ev.Title} for club {club.Name}");
    }
}
