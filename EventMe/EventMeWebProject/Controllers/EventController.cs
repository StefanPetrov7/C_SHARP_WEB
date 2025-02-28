using Microsoft.AspNetCore.Mvc;

using EventMiServicesData.Contracts;
using EventMiViewModels.Event;
using System.Globalization;

namespace EventMeWebProject.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventFromModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);  // reload the same page with the model errors 
            }

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);

            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format");
                return View(model);
            }

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format");
                return View(model);
            }

            await this.eventService.AddEvent(model, startDate, endDate);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                EditEventFormModel eventModel = await this.eventService.GetEventById(id.Value);
                return View(eventModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, EditEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);

            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format");
                return View(model);
            }

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format");
                return View(model);
            }

            try
            {
                await this.eventService.EditEventById(id.Value, model, startDate, endDate);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                EditEventFormModel eventModel = await this.eventService.GetEventById(id.Value);
                return View(eventModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, EditEventFormModel model)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                await this.eventService.DeleteEventById(id.Value);

                // Implement view all functionality and redirect 

                return RedirectToAction("Index", "Home");

            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

    }
}



