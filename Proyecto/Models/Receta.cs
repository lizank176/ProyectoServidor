using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Receta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Tiempo (minutos)")]
        [Range(1, 1000, ErrorMessage = "El tiempo debe ser mayor a 0")]
        public int TiempoCoccion { get; set; }

        public Dificultad Dificultad { get; set; }

        // Aquí guardaremos la ruta de la imagen en wwwroot/uploads
        public string? ImagenUrl { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // --- RELACIONES ---

        // ID del Usuario de Identity que creó la receta
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        // Una receta tiene muchos ingredientes
        public List<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    }
}
