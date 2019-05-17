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

        // GET: api/todo
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


    }

}
