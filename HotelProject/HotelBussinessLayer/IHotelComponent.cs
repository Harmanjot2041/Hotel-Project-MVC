using Entities.Custom;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBussinessLayer
{
    public interface IHotelComponent
    {
        List<CustomRoom> SearchedRooms(DateCheckModel model);
        List<Testimonials> GetTestimonials();
        int AddTestimonials(Testimonials model);
        Room GetAvailableRoomByType(CustomRoomCheck model);
        long AddPerson(Person model);
        int AddBooking(Booking model);
    }
}
