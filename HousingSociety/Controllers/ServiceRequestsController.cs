
using HousingSociety.Contracts;
using HousingSociety.Application.Interfaces;
using HousingSociety.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HousingSociety.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController : ControllerBase
{
    private readonly IServiceRequestService _service;
    public ServiceRequestsController(IServiceRequestService service) => _service = service;

    // GET /api/servicerequests?status=Open&type=Plumbing&societyId=...&residentId=...
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceRequestDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceRequestDto>>> GetAll(
        [FromQuery] RequestStatus? status,
        [FromQuery] RequestType? type,
        [FromQuery] Guid? societyId,
        [FromQuery] Guid? residentId)
        => Ok(await _service.GetAllAsync(status, type, societyId, residentId));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ServiceRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceRequestDto>> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // GET /api/residents/{residentId}/servicerequests
    [HttpGet("/api/residents/{residentId:guid}/servicerequests")]
    [ProducesResponseType(typeof(IEnumerable<ServiceRequestDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceRequestDto>>> GetByResident(Guid residentId)
        => Ok(await _service.GetByResidentAsync(residentId));

    [HttpPost]
    [ProducesResponseType(typeof(ServiceRequestDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceRequestDto>> Create([FromBody] CreateServiceRequestDto dto)
    {
        if (dto.ResidentId == Guid.Empty) return BadRequest("ResidentId is required.");
        if (string.IsNullOrWhiteSpace(dto.Title)) return BadRequest("Title is required.");
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateServiceRequestStatusDto dto)
    {
        // Optional: simple guard (business rules will live in the service)
        if (!Enum.IsDefined(typeof(RequestStatus), dto.Status))
            return BadRequest("Invalid status.");

        var ok = await _service.UpdateStatusAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
