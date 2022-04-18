using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TiendaServicios.Api.Libro.Aplication;
using TiendaServicios.Api.Libro.CTX;
using TiendaServicios.Api.Libro.Model;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServicesTest
    {
        //SImpre se devuelve un dto al cliente
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle();


            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = 0;

            return lista;

        }

        private Mock<ContextoLibreria> CrearContexto()
        {

            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
            .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            //Esto es para hacer consultas getOne
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));


            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.libreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async void GetLibros()
        {
            //System.Diagnostics.Debugger.Launch();
            // que metodo dentro de mi microservice libro se esta tratando de simular  "Clase  Consulta"
            // 1. Emular la instacion de la persistencia  ContextoLibreria
            // para emular las acciones y eventos de un objeto se utiliza objetos de tipo MOCK instalamos paquete MOQ

            //var mockContexto = new Mock<ContextoLibreria>();
            var mockContexto = CrearContexto();

            // 2. Emular  al mapping IMaper
            var mapConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingTest());  
            });
            var mapper = mapConfig.CreateMapper();

            //3 instanciar a la clase Manejador y pasarle como parametros  los mocks que se han creado

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();
            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.True(lista.Any());//validar se se devuelve algun valor ,el Any me devuelve true o false
            

        }

        [Fact]
        public async void GetLibroPorId()
        {
            //System.Diagnostics.Debugger.Launch();
            var mockContexto = CrearContexto();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = 0;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            //Assert.True(libro.LibreriaMaterialId == 0);

        }

        [Fact] 
        public async void GuardarLibro()
        {
            //System.Diagnostics.Debugger.Launch();


            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro  test de Microservice";
            request.AutorLibroId = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null);
        }

    }
}