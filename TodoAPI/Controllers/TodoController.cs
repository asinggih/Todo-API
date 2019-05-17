using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController] // This decorator shows that it respons to web API request
    public class TodoController : ControllerBase {

        private readonly TodoContext _context;

        public TodoController(TodoContext context) {

            _context = context;

            // If there's nothing in the DB
            if (_context.TodoItems.Count() == 0) {

                // Hard-coding the addition the the db just for example
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();

            }
        }

        /* GET: api/todo
         * 
         * having ActionResult<T> return type automatically serialises
         * the object into JSON, as well as the respective response code
         * this can be 200 OK or 5xx error, etc etc.
         * 
         */         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {

            return await _context.TodoItems.ToListAsync();

        }

        /* GET: api/todo/7
         *        
         * specify what's gonna be the "args" in url
         * domain.com/api/todo/{id}        
         *       
        */
        [HttpGet("{id}")] 
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id) {

            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem is null) {
                return NotFound();
            }

            return todoItem;

        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item) {

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = item.Id },
                item
            );
        }

        // PUT: api/todo/7
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item) {

            if (id != item.Id) {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // To_do
        // PATCH: api/todo/7


        // DELETE: api/todo/7
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {

            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem is null) {
                return NotFound();
            }

            // actually removing it from the DB
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }

}
