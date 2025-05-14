using DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[Route("api/[controller]")]
[ApiController]
public class FormsController : ControllerBase
{
    private readonly FormService _service;
    
    /// <inheritdoc />
    public FormsController(FormService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all sectors
    /// </summary>
    /// <returns>List of sectors</returns>
    [HttpGet("sectors")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Sector>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Sector>>> GetSectorsAsync()
    {
        return Ok(await _service.GetSectorsAsync());
    }
    
    /// <summary>
    /// Create new form
    /// </summary>
    /// <param name="form"></param>
    /// <returns>Form</returns>
    [HttpPost]
    [Produces( "application/json" )]
    [Consumes("application/json")]
    [ProducesResponseType( typeof(Form), StatusCodes.Status200OK)]
    public async Task<ActionResult<Form>> PostFormAsync(FormCreate form)
    {
        var res = await _service.PostFormAsync(form);
        return Ok(res);
    }
    
    /// <summary>
    /// Create new form's sectors
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Task</returns>
    [HttpPost("sector-in-form")]
    [Produces( "application/json" )]
    [Consumes("application/json")]
    [ProducesResponseType( StatusCodes.Status204NoContent)]
    public async Task<IActionResult>  PostFormSectorsAsync(FormWithSectors dto)
    {
        await _service.PostSectorInFormsAsync(dto.Form, dto.SectorIds);
        return NoContent();
    }
    
    /// <summary>
    /// Update form and its sectors
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Sector in form</returns>
    [HttpPut]
    [Produces( "application/json" )]
    [Consumes("application/json")]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> PutSectorInFormAsync(FormWithSectors dto)
    {
        await _service.UpdateFormWithSectorsAsync(dto.Form, dto.SectorIds);
        return NoContent();
    }
    
    /// <summary>
    /// Get form info by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>FormWithSectors</returns>
    [HttpGet("form-with-sectors/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(FormWithSectors), StatusCodes.Status200OK)]
    public async Task<ActionResult<FormWithSectors>> GetFormWithSectorsAsync(int id)
    {
        return Ok(await _service.GetFormWithSectorsAsync(id));
    }
}