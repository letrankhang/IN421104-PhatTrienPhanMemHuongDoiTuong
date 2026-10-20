using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositorys.Impl;

public sealed class InMemoryMemberRepositoryImpl : IMemberRepository
{
    private readonly List<Member> _store = new();

    public IReadOnlyList<Member> GetByClubId(Guid clubId)
    {
        return _store.Where(m => m.ClubId == clubId).ToList().AsReadOnly();
    }
    public Member? GetById(Guid memberId)
    {
        return _store.FirstOrDefault(m => m.Id == memberId);
    }
    public void Add(Member member)
    {
        _store.Add(member);
    }
}