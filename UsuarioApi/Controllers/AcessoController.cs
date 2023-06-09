using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuarioApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AcessoController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "IdadeMinima")]
    public IActionResult get()
    {
        return Ok("acesso permitido");

    }
}