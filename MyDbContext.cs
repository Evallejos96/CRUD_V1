using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace CRUD
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=dbConnect")
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
    }

}
