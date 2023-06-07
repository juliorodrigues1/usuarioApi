using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos;

using UsuarioApi.Services;

namespace UsuarioApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
   
    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> create([FromBody] CreateUsuarioDto dto)
    {
        await _usuarioService.Cadastro(dto);
        return Ok("Usuario cadastrado");

    }

    [HttpPost("/login")]
    public async Task<IActionResult> login(LoginUsuarioDto dto)
    {
        var token = await _usuarioService.Login(dto);
        return Ok(token);

    }
}