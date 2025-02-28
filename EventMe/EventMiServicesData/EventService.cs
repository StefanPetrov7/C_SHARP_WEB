using Microsoft.EntityFrameworkCore;

using EventMeData;
using EventMeDataModels;
using EventMiServicesData.Contracts;
using EventMiViewModels.Event;

namespace EventMiServicesData
{
    public class EventService : IEventService
    {
        private readonly EventMeDbContext dbContext;

        public EventService(EventMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddEvent(AddEventFromModel eventFormModel, DateTime startDate, DateTime endDate)
        {

            Event newEvent = new Event()
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = endDate,
                Place = eventFormModel.Place,
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();

        }

        public async Task<EditEventFormModel> GetEventById(int id)
        {
            Event? eventDb = await this.dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventDb == null)
            {
                throw new ArgumentException();
            }

            if (!eventDb.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            EditEventFormModel eventModel = new EditEventFormModel()
            {
                Name = eventDb.Name,
                StartDate = eventDb.StartDate.ToShortDateString(),
                EndDate = eventDb.EndDate.ToShortDateString(),
                Place = eventDb.Place,
            };

            return eventModel;

        }

        public async Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event eventToEdit = await dbContext.Events.FirstAsync(x => x.Id == id);

            if (!eventToEdit.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            eventToEdit.Name = eventFormModel.Name;
            eventToEdit.StartDate = startDate;
            eventToEdit.EndDate = endDate;
            eventToEdit.Place = eventFormModel.Place;

            await dbContext.SaveChangesAsync();

        }

        public async Task DeleteEventById(int id)
        {
            Event? eventToDelete = await this.dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (eventToDelete == null)
            {
                throw new ArgumentNullException();
            }

            if (!eventToDelete.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            this.dbContext.Events.Remove(eventToDelete);
            await this.dbContext.SaveChangesAsync();

        }
    }
}
