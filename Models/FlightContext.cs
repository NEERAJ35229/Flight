using Microsoft.EntityFrameworkCore;
using System;

namespace Flight.Models
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<FlightModel> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightModel>().HasData(

                new FlightModel
                {
                    FlightId = 1,
                    FlightNumber = "UA3321",
                    From = "Chicago",
                    To = "New York",
                    Date = new DateTime(2026, 2, 15),
                    Price = 235
                },

                new FlightModel
                {
                    FlightId = 2,
                    FlightNumber = "QA1078",
                    From = "Dubai",
                    To = "London",
                    Date = new DateTime(2026, 3, 1),
                    Price = 590
                },

                new FlightModel
                {
                    FlightId = 3,
                    FlightNumber = "CA9087",
                    From = "Hong Kong",
                    To = "San Francisco",
                    Date = new DateTime(2026, 6, 15),
                    Price = 900
                }

            );
        }
    }
}