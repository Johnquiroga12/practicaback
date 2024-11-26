using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;
using System.Threading.Tasks;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TaskController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // Obtener todas las tareas
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tareas = await _tareaService.GetAllTasks();
            return Ok(tareas);
        }

        // Obtener tarea por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var tarea = await _tareaService.GetTaskById(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        // Crear nueva tarea
[HttpPost]
public async Task<IActionResult> CreateTask([FromBody] Tarea tarea)
{
    // Asegúrate de que el servicio genere el id automáticamente
    var createdTask = await _tareaService.CreateTask(tarea);

    // Devuelve la tarea creada con el id generado
    return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
}


        // Actualizar tarea existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Tarea tarea)
        {
            var updatedTask = await _tareaService.UpdateTask(id, tarea);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        // Eliminar tarea
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _tareaService.DeleteTask(id);
            if (result)
            {
                return NoContent();  // Eliminado correctamente
            }
            return NotFound();  // No encontrado
        }
    }
}
