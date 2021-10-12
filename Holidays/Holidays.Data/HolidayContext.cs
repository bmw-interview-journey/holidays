using Holidays.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Holidays.Data
{
    public class HolidayContext : DbContext
    {
        public DbSet<Holiday> Holidays { get; set; }

        public HolidayContext(DbContextOptions<HolidayContext> options) : base(options)
        {
        }
    }
}
