using EduHub.Application.Clubs;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

/// <summary>Quản lý câu lạc bộ. Controller chỉ nhận request, gọi handler của Application rồi trả status code.</summary>
[ApiController]
[Route("api/clubs")]
public class ClubsController : ControllerBase
{
    private readonly GetClubsQueryHandler _getClubsHandler;
    private readonly GetClubByIdQueryHandler _getClubByIdHandler;
    private readonly CreateClubCommandHandler _createClubHandler;
    private readonly DeleteClubCommandHandler _deleteClubHandler;

    public ClubsController(
        GetClubsQueryHandler getClubsHandler,
        GetClubByIdQueryHandler getClubByIdHandler,
        CreateClubCommandHandler createClubHandler,
        DeleteClubCommandHandler deleteClubHandler)
    {
        _getClubsHandler = getClubsHandler;
        _getClubByIdHandler = getClubByIdHandler;
        _createClubHandler = createClubHandler;
        _deleteClubHandler = deleteClubHandler;
    }

    // Các method dưới đây chưa có async để starter build được không cảnh báo.
    // Khi viết thân method, hãy thêm từ khoá async vào chữ ký.

    /// <summary>Lấy danh sách câu lạc bộ.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Club>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll(CancellationToken ct)
    {
        // TODO 8a: gọi _getClubsHandler.HandleAsync(new GetClubsQuery(), ct)
        // TODO 8b: trả Ok(clubs) → 200
        throw new NotImplementedException();
    }

    /// <summary>Lấy một câu lạc bộ theo Id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Club), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        // TODO 8c: gọi _getClubByIdHandler.HandleAsync(new GetClubByIdQuery(id), ct)
        // TODO 8d: không có club → NotFound() (404); có → Ok(club) (200)
        throw new NotImplementedException();
    }

    /// <summary>Tạo câu lạc bộ mới.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Club), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Create([FromBody] CreateClubRequest request, CancellationToken ct)
    {
        // TODO 8e: đổi request thành CreateClubCommand, gọi _createClubHandler.HandleAsync(command, ct)
        // TODO 8f: thành công → CreatedAtAction(nameof(GetById), new { id = club.Id }, club) (201 + Location)
        // TODO 8g: bắt ArgumentException → BadRequest(new { message = ex.Message }) (400)
        throw new NotImplementedException();
    }

    /// <summary>Xoá một câu lạc bộ.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        // TODO 8h: gọi _deleteClubHandler.HandleAsync(new DeleteClubCommand(id), ct) → NoContent() (204)
        // TODO 8i: bắt KeyNotFoundException → NotFound() (404)
        throw new NotImplementedException();
    }
}

/// <summary>Dữ liệu client gửi lên khi tạo CLB (DTO/mapping đầy đủ sẽ học ở Chương 4).</summary>
public record CreateClubRequest(string Name, string Description);
