using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPICrud.Models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

namespace WebAPICrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly CrudContext crudContext;
        public EmpleadoController(CrudContext _crudContext)
        {
            crudContext = _crudContext;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaEmpleado = await crudContext.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaEmpleado);
        }


        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var empleado = await crudContext.Empleados.FirstOrDefaultAsync(e=>e.IdEmpleado==id);
            return StatusCode(StatusCodes.Status200OK, empleado);
        }


        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody]Empleado objeto)
        {
            await crudContext.Empleados.AddAsync(objeto);
            await crudContext.SaveChangesAsync();
            
            return StatusCode(StatusCodes.Status200OK, new {mensake = "ok"});
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Empleado objeto)
        {
            crudContext.Empleados.Update(objeto);
            await crudContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensake = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var empleado = await crudContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            crudContext.Empleados.Remove(empleado);
            await crudContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensake = "ok" });
        }

    }
}
