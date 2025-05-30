using JaveragesLibrary.Domain.Entities;
using JaveragesLibrary.Services.Features.Figuras;
using Microsoft.AspNetCore.Mvc;

namespace JaveragesLibrary.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class FiguraController : ControllerBase
{
    private readonly FiguraService _figuraService;

    public FiguraController(FiguraService figuraService)
    {
        _figuraService = figuraService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var figuras = _figuraService.GetAll();
        return Ok(figuras);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var figura = _figuraService.GetById(id);
        if (figura == null)
            return NotFound(new { Message = $"Figura con ID {id} no encontrada." });

        return Ok(figura);
    }

    [HttpPost]
    public IActionResult Add([FromBody] Figura figura)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var nueva = _figuraService.Add(figura);
        return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Figura figura)
    {
        if (id != figura.Id)
            return BadRequest(new { Message = "ID de ruta no coincide con el ID del cuerpo." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = _figuraService.Update(figura);
        if (!success)
            return NotFound(new { Message = $"Figura con ID {id} no encontrada para actualizar." });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var success = _figuraService.Delete(id);
        if (!success)
            return NotFound(new { Message = $"Figura con ID {id} no encontrada para eliminar." });

        return NoContent();
    }
}
