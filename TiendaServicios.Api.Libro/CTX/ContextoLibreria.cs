using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.CTX
{
    public class ContextoLibreria :DbContext
    {
        public ContextoLibreria() { }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options): base(options) { }

        public virtual DbSet<LibreriaMaterial> libreriaMaterial { get; set; }// se puede sobreescribir virtual
    }
}
