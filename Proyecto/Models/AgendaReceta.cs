using Microsoft.AspNetCore.Identity;

namespace Proyecto.Models
{
    public class AgendaReceta
    {
        public int Id { get; set; }

        // Usuario que guarda la receta
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        // Receta guardada
        public int RecetaId { get; set; }
        public Receta? Receta { get; set; }

        public DateTime FechaGuardado { get; set; } = DateTime.Now;
    }
}