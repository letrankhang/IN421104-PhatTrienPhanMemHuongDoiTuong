using EduHub.Ch02.Domain;
public class InMemoryMemberRepositoryImpl : IMemberRepository
{
    private readonly List<Member> _store = new();
    public IReadOnlyList<Member> GetByClubId(Guid clubId)
    {
        return _store.Where(m => m.ClubId == clubId).ToList().AsReadOnly();
    }
    public Member? GetById(Guid Id)
    {
        return _store.FirstOrDefault(m => m.Id == Id);
    }
    public void Add(Member member)
    {
        _store.Add(member);
    }
}