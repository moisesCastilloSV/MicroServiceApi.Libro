using MediatR;
using TiendaServicios.Api.Libro.CTX;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class Nuevo
    {
        public class Ejecuta : IRequest  
        { 
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid AutorLibroId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contextoLibreria;

            public Manejador(ContextoLibreria contextoLibreria)
            {
                _contextoLibreria=contextoLibreria;
            }
            public async Task<Unit> Handle(Ejecuta request,CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibroId = request.AutorLibroId
                };
                _contextoLibreria.libreriaMaterial.Add(libro);
                var resultado = await _contextoLibreria.SaveChangesAsync();

                if (resultado > 0) { return Unit.Value; }

                throw new Exception("No se pudo guarda el libro");


            }
        }
    }
}
