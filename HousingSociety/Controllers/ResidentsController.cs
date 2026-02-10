
using HousingSociety.Contracts;
using HousingSociety.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HousingSociety.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResidentsController : ControllerBase
{
    private readonly IResidentService _service;
    public ResidentsController(IResidentService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResidentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResidentDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResidentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResidentDto>> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("/api/societies/{societyId:guid}/residents")]
    [ProducesResponseType(typeof(IEnumerable<ResidentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResidentDto>>> GetBySociety(Guid societyId)
        => Ok(await _service.GetBySocietyAsync(societyId));

    [HttpPost]
    [ProducesResponseType(typeof(ResidentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResidentDto>> Create([FromBody] CreateResidentDto dto)
    {
        if (dto.SocietyId == Guid.Empty) return BadRequest("SocietyId is required.");
        if (string.IsNullOrWhiteSpace(dto.FullName)) return BadRequest("FullName is required.");
        if (string.IsNullOrWhiteSpace(dto.Email)) return BadRequest("Email is required.");
        if (string.IsNullOrWhiteSpace(dto.Phone)) return BadRequest("Phone is required.");
        if (string.IsNullOrWhiteSpace(dto.FlatNumber)) return BadRequest("FlatNumber is required.");
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            
            return StatusCode(500, e.Message);
        }
        
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResidentDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
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
