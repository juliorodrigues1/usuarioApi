using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UsuarioApi.Models;

public class Usuario : IdentityUser
{
    public Usuario() : base()
    {
    }

    public DateTime DataNascimento { get; set; }
    
}