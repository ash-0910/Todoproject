using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoproject.Models;

namespace Todoproject.Infra
{
    public class todocontext : DbContext
    {
        // context for database connection

        public todocontext(DbContextOptions<todocontext> options)
            : base(options)
        {
        }

        public DbSet<todolist> ToDoList { get; set; }
    }
}
