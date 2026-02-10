
using HousingSociety.Contracts;
using HousingSociety.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HousingSociety.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SocietiesController : ControllerBase
{
    private readonly ISocietyService _service;
    public SocietiesController(ISocietyService service) => _service = service;

    /// <summary>Get all societies.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SocietyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SocietyDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    /// <summary>Get a society by ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SocietyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SocietyDto>> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Create a new society.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(SocietyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SocietyDto>> Create([FromBody] CreateSocietyDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required.");
        if (string.IsNullOrWhiteSpace(dto.Address)) return BadRequest("Address is required.");
        if (string.IsNullOrWhiteSpace(dto.City)) return BadRequest("City is required.");
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

    /// <summary>Update a society.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSocietyDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Delete a society.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}