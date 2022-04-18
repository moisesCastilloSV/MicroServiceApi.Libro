using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.CTX;
using TiendaServicios.Api.Libro.Dto;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class ConsultaFiltro
    {

        public class LibroUnico : IRequest<LibreriaMaterialDto>
        {
            public int LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibreriaMaterialDto>
        {

            private readonly ContextoLibreria _contextoLibro;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contextoLibro, IMapper mapper)
            {
                _contextoLibro = contextoLibro;
                _mapper = mapper;
            }

            public async Task<LibreriaMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contextoLibro.libreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if (libro == null) { throw new Exception("No se Encontro el Libro"); }
                var libroDto = _mapper.Map<LibreriaMaterial, LibreriaMaterialDto>(libro);

                return libroDto;
            }
        }

    }
}
