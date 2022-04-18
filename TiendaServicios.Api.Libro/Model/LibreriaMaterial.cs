using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.Api.Libro.Model
{
    public class LibreriaMaterial
    {
        [Key]
        public  int LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public Guid? AutorLibroId { get; set; }
    }
}
