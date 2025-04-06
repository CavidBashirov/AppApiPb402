﻿using AppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
