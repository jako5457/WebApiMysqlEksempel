using Datalayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer
{
    public class DataContext : DbContext
    {
        public DbSet<Temprature> Tempratures { get; set; }

        public DbSet<Humidity> Humidities { get; set; }

        public DbSet<Pressure> Pressures { get; set; }
    }
}
