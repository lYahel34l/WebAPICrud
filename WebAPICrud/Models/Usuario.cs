using System.ComponentModel.DataAnnotations;

namespace WebAPICrud.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Contra { get; set; }

    }
}
