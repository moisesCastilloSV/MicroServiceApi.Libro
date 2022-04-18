using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.CTX;
using TiendaServicios.Api.Libro.Dto;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class Consulta
    {

        public class Ejecuta : IRequest<List<LibreriaMaterialDto>>{ }
        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDto>>
        {
            private readonly ContextoLibreria _contextoLibro;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contextoLibro, IMapper mapper)
            {
                _contextoLibro = contextoLibro;
                _mapper = mapper;
            }
            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contextoLibro.libreriaMaterial.ToListAsync();
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto>>(libros);

                return librosDto;
            }
        }
    }
}
