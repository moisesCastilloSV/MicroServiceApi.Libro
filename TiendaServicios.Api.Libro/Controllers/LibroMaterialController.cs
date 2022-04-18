using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplication;
using TiendaServicios.Api.Libro.Dto;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibroMaterialController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        // Metodo de insert
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        // Metodo de getAll

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<LibreriaMaterialDto>>> getLibros()
        {
            try { return await _mediator.Send(new Consulta.Ejecuta());  } catch(Exception) { return NotFound(); }
            
        }

        // Metodo de  getOne
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LibreriaMaterialDto>> getLibro(string id)
        {
            try {
                return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = Int32.Parse(id) }); 
            } catch(Exception) 
            {
                return NotFound();
                //return StatusCode(500);
            }
            
        }
    }


}

