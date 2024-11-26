using TaskManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Services
{
    public class TareaService : ITareaService
    {
        private readonly ApplicationDbContext _context;

        public TareaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllTasks()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task<Tarea> GetTaskById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Tareas
                                 .FirstOrDefaultAsync(t => t.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

public async Task<Tarea> CreateTask(Tarea tarea)
{
    _context.Tareas.Add(tarea); // La base de datos asignará el 'id' automáticamente
    await _context.SaveChangesAsync(); // Guarda la tarea en la base de datos
    return tarea; // La tarea ahora tiene un 'id' generado
}

        public async Task<Tarea> UpdateTask(int id, Tarea tarea)
        {
            var existingTarea = await _context.Tareas
                                              .FirstOrDefaultAsync(t => t.Id == id);
            if (existingTarea == null)
            {
                return null; // O podrías lanzar una excepción personalizada
            }

            existingTarea.Title = tarea.Title;
            existingTarea.Description = tarea.Description;
            existingTarea.DueDate = tarea.DueDate;
            existingTarea.IsCompleted = tarea.IsCompleted;

            await _context.SaveChangesAsync();
            return existingTarea;
       }

        public async Task<bool> DeleteTask(int id)
        {
            var tarea = await _context.Tareas
                                      .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
                return true;  // Se eliminó correctamente
            }

            return false;  // No se encontró la tarea
        }
    }
}
