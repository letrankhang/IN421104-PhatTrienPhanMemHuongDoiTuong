using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public interface IClubService
{
    Club CreateClub(string name, string description, string presidentEmail);
    Member AddMembertoClub(Guid clubId, string name, string email);
}