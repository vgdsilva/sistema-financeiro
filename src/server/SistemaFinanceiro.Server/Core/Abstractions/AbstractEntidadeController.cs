using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Dominio.Entidades;

namespace SistemaFinanceiro.Server.Core.Abstractions;

public abstract class AbstractEntidadeController<T> : ControllerBase where T : EntidadeControle
{

    [HttpGet("find/{Id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<T>> Find(Guid Id, CancellationToken cancellationToken = default)
    {
        return Ok();
    }

    [HttpGet("find/{From:int}/{To:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<T>> FindRange(int From, int To, CancellationToken cancellationToken = default)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<T>> Create([FromBody] T entity, CancellationToken cancellationToken = default)
    {
        return CreatedAtAction(nameof(Find), new { Id = entity.Id }, entity);
    }

    [HttpPost("edit/{Id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<T>> Edit(Guid Id, [FromBody] T entity, CancellationToken cancellationToken = default)
    {
        return Ok(entity);
    }

    [HttpDelete("remove/{Id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(Guid Id, CancellationToken cancellationToken = default)
    {
        return NoContent();
    }
}
