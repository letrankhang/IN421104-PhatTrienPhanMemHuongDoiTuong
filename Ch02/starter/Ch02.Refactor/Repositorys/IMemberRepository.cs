using EduHub.Ch02.Domain;
public interface IMemberRepository
{
    IReadOnlyList<Member> GetByClubId(Guid clubId);
    Member? GetById(Guid Id);
    void Add(Member member);
}