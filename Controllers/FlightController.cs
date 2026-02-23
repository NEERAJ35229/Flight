using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flight.Models;

namespace Flight.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightContext context;

        public FlightController(FlightContext ctx)
        {
            context = ctx;
        }

        // ================================
        // INDEX - View All Flights
        // ================================
        public IActionResult Index()
        {
            var flights = context.Flights
                                 .OrderBy(f => f.FlightNumber)
                                 .ToList();

            return View(flights);
        }

        // ================================
        // ADD - GET
        // ================================
        [HttpGet]
        public IActionResult Add()
        {
            LoadCities();
            return View(new FlightModel());
        }

        // ================================
        // EDIT - GET (Using Slug)
        // ================================
        [HttpGet]
        public IActionResult Edit(string id)
        {
            int flightId = int.Parse(id.Split('-').Last());

            var flight = context.Flights.Find(flightId);

            if (flight == null)
                return RedirectToAction("Index");

            LoadCities();
            return View(flight);
        }

        // ================================
        // ADD + EDIT - POST
        // ================================
        [HttpPost]
        public IActionResult Edit(FlightModel flight)
        {
            if (ModelState.IsValid)
            {
                if (flight.FlightId == 0)
                    context.Flights.Add(flight);
                else
                    context.Flights.Update(flight);

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadCities();
            return View(flight.FlightId == 0 ? "Add" : "Edit", flight);
        }

        // ================================
        // DELETE - GET (Using Slug)
        // ================================
        [HttpGet]
        public IActionResult Delete(string id)
        {
            int flightId = int.Parse(id.Split('-').Last());

            var flight = context.Flights.Find(flightId);

            if (flight == null)
                return RedirectToAction("Index");

            return View(flight);
        }

        // ================================
        // DELETE - POST
        // ================================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var flight = context.Flights.Find(id);

            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ================================
        // LOAD CITIES FOR DROPDOWN
        // ================================
        private void LoadCities()
        {
            var cities = new List<string>
            {
                "Chicago",
                "New York",
                "Dubai",
                "London",
                "Hong Kong",
                "San Francisco"
            };

            ViewBag.Cities = new SelectList(cities);
        }
    }
}