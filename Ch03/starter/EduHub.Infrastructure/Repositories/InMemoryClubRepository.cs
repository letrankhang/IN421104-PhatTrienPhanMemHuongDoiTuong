using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Kho CLB lưu tạm trong bộ nhớ — dùng cho Chương 3 khi chưa có database.
/// Chương 4 sẽ thay bằng bản dùng EF Core và PostgreSQL.
/// </summary>
public class InMemoryClubRepository : IClubRepository
{
    // static: repository đăng ký Scoped (mỗi request một bản mới) nhưng dữ liệu phải sống suốt vòng đời ứng dụng.
    private static readonly List<Club> _clubs = new();
    private static readonly object _lock = new();

    public Task<IEnumerable<Club>> GetAllAsync(CancellationToken ct = default)
    {
       lock (_lock)
        {
            // TODO 1a: trả về danh sách tất cả club
            return Task.FromResult<IEnumerable<Club>>(_clubs.ToList());
        }
    }

    public Task<Club?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
        {
            // TODO 1b: tìm club theo Id; không thấy thì trả null, KHÔNG throw
            var club = _clubs.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(_clubs.FirstOrDefault(c => c.Id == id));
        }
    }

    public Task AddAsync(Club club, CancellationToken ct = default)
    {
        // TODO 1c: thêm club vào _clubs
        lock (_lock)
        {
            _clubs.Add(club);
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Club club, CancellationToken ct = default)
    {
        // TODO 1d: tìm club cũ theo Id và thay bằng club mới; không thấy thì throw KeyNotFoundException
        lock (_lock)
        {
            var index = _clubs.FindIndex(c => c.Id == club.Id);
            if (index < 0)
                throw new KeyNotFoundException($"KHông tìm thấy club {club.Id}");
            _clubs[index] = club;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var club = _clubs.FirstOrDefault(c => c.Id == id)
                ?? throw new KeyNotFoundException($"Không tìm thấy club {id}");
            _clubs.Remove(club);
        }
        // TODO 1e: xoá club theo Id; không thấy thì throw KeyNotFoundException
        return Task.CompletedTask;
    }
}
