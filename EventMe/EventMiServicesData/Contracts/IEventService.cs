using System;
using EventMiViewModels.Event;

namespace EventMiServicesData.Contracts
{
    public interface IEventService
    {
        Task AddEvent(AddEventFromModel eventFormModel, DateTime startDate, DateTime endDate);

        Task<EditEventFormModel> GetEventById(int id);

        Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate);

        Task DeleteEventById(int id);

    }
}
