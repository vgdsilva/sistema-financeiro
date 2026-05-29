using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Dominio.Entidades;
using SistemaFinanceiro.Server.Core.Abstractions;

namespace SistemaFinanceiro.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsuarioController : AbstractEntidadeController<Usuario>
{
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
    }

    //[HttpGet]
    //[AllowAnonymous]
    //[ProducesResponseType(typeof(PagedResult<UsuarioDto>), StatusCodes.Status200OK)]
    //public async Task<ActionResult<PagedResult<BookDto>>> GetUsuarios([FromQuery] BookSearchCriteria criteria, CancellationToken cancellationToken = default)
    //{
    //    return Ok(result);
    //}

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<Usuario>> GetUsuario(int id, CancellationToken cancellationToken = default)
    //{
    //    return Ok();
    //}

    //[HttpPost("{id:Guid}")]
    //[Authorize(Roles = "User,Administrator")]
    //[ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<Usuario>> SaveUsuario([FromBody] Usuario usuario, CancellationToken cancellationToken = default)
    //{
    //    return Ok(usuario);
    //}

    //[HttpDelete("{id:int}")]
    //[Authorize(Roles = "Administrator")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteUsuario(Guid id, CancellationToken cancellationToken = default)
    //{
    //    return NoContent();
    //}
}
