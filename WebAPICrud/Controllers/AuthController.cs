using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebAPICrud.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CrudContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(CrudContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Usuario usuario)
    {
        var usuarioExistente = await _context.Usuarios  // Change from crudContext to _context
            .FirstOrDefaultAsync(u => u.NombreUsuario == usuario.NombreUsuario);

        if (usuarioExistente == null)
            return Unauthorized(new { mensaje = "Usuario no encontrado" });

        if (usuario.Contra.Trim() != usuarioExistente.Contra.Trim())
            return Unauthorized(new { mensaje = "Contraseña incorrecta" });

        return Ok(new { mensaje = "Inicio de sesión exitoso" });
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // Simple comparison method
        return inputPassword.Trim() == storedPassword.Trim();

        // For more secure methods, consider:
        // 1. Case-insensitive comparison
        // return string.Equals(inputPassword.Trim(), storedPassword.Trim(), StringComparison.OrdinalIgnoreCase);

        // 2. Hashing (recommended for production)
        // Hash input password and compare with stored hash
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        // Implementación de generación de token JWT
        // Se requiere configuración adicional de servicios de autenticación
        return string.Empty;
    }

    public class LoginDto
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
    }
}