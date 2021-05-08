using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreMVC.Models;

namespace testCoreMVC.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
             : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public object Movies { get; internal set; }
    }
}

