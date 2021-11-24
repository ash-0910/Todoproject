using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoproject.Infra;
using Todoproject.Models;

namespace Todoproject.Controllers
{
    public class todocontroller : Controller
    {
        private readonly todocontext context;

        public todocontroller(todocontext context)
        {
            this.context = context;
        }

        // get data from database/
        public async Task<ActionResult> Index()
        {
            IQueryable<todolist> items = from i in context.ToDoList orderby i.Id select i;

            List<todolist> todoList = await items.ToListAsync();

            return View(todoList);

        }

        // create a new data 
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(todolist item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "added successfully";

                return RedirectToAction("Index");
            }

            return View(item);

        }


        //edit data 
        public async Task<ActionResult> Edit(int id)
        {
            todolist item = await context.ToDoList.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }


        // show the edited data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(todolist item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "edited successfully";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //delete data 
        public async Task<ActionResult> Delete(int id)
        {
            todolist item = await context.ToDoList.FindAsync(id);
            if (item != null)
            {
                
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "deleted successfully";
            }

            return RedirectToAction("Index");
        }

    }
}
