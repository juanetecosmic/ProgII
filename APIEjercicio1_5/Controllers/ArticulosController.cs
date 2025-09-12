using Ejercicio1_5.Domain;
using Ejercicio1_5.Services;
using Ejercicio1_5.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIEjercicio1_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private IArticuloService _articuloService;
        public ArticulosController(IArticuloService service)
        {
            _articuloService = service;
        }
        // GET: api/<ArticulosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_articuloService.GetAll());
        }

        // GET api/<ArticulosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_articuloService.GetById(id));
        }

        // POST api/<ArticulosController>
        [HttpPost]
        public IActionResult Post([FromBody] Articulo articulo)
        {
            return Ok(_articuloService.Save(articulo));
        }

        // PUT api/<ArticulosController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Articulo articulo)
        {
            return Ok(_articuloService.Save(articulo));
        }

        // DELETE api/<ArticulosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_articuloService.Delete(id));
        }
    }
}
