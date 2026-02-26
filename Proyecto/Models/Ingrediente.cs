namespace Proyecto.Models
{
    public class Ingrediente
    {
        public int Id { get; set; } // Revisa esta línea 5
        public string Nombre { get; set; } = string.Empty;
        public string Cantidad { get; set; } = string.Empty;
        public int RecetaId { get; set; }
        public Receta? Receta { get; set; }
    }
}
