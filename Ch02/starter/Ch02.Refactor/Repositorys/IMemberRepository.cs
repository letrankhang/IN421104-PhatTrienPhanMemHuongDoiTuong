using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositorys;

public interface IMemberRepository
{
    IReadOnlyList<Member> GetByClubId(Guid clubId);
    Member? GetById(Guid memberId);
    void Add(Member member);
}