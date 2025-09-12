using Ejercicio1_5.Domain;
using Ejercicio1_5.Services;
using Ejercicio1_5.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIEjercicio1_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private IFacturaService _facturaService;
        public FacturasController(IFacturaService service)
        {
            _facturaService = service;
        }
        // GET: api/<FacturasController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_facturaService.GetAll());
        }

        // GET api/<FacturasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_facturaService.GetById(id));
        }

        // POST api/<FacturasController>
        [HttpPost]
        public IActionResult Post([FromBody] Factura factura)
        {
            return Ok(_facturaService.Save(factura));
        }
    }
}
