using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UsuarioApi.Models;

namespace UsuarioApi.Services;

public class TokenService
{
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id),
            new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
        };
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kasjdklasndalskdnaldknaldknafjhdjfljksdjflskdfj"));
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials:signingCredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}