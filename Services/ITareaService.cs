using TaskManagerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagerAPI.Services
{
    public interface ITareaService
    {
        // Obtener todas las tareas
        Task<IEnumerable<Tarea>> GetAllTasks();

        // Obtener una tarea por su ID
        Task<Tarea> GetTaskById(int id);

        // Crear una nueva tarea
        Task<Tarea> CreateTask(Tarea tarea);

        // Actualizar una tarea existente
        Task<Tarea> UpdateTask(int id, Tarea tarea);
    
      // Eliminar una tarea por su ID
        Task<bool> DeleteTask(int id);
    }

}
