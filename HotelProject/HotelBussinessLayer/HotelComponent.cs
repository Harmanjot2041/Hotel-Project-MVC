using Entities.Custom;
using Entities.Entities;
using HotelDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBussinessLayer
{
    public class HotelComponent:IHotelComponent
    {
        private readonly IHotelDataAccess _hotelDataAccess;
        public HotelComponent(IHotelDataAccess hotelDataAccess)
        {
            _hotelDataAccess = hotelDataAccess;
        }

        public int AddTestimonials(Testimonials model)
        {
            return _hotelDataAccess.AddTestimonials(model);
        }

        public List<CustomRoom> SearchedRooms(DateCheckModel model)
        {
            return _hotelDataAccess.SearchedRooms(model);
        }

        public List<Testimonials> GetTestimonials()
        {
            return _hotelDataAccess.GetTestimonials();
        }
        public Room GetAvailableRoomByType(CustomRoomCheck model)
        {
            return _hotelDataAccess.GetAvailableRoomByType(model);
        }

        public long AddPerson(Person model)
        {
            return _hotelDataAccess.AddPerson(model);
        }

        public int AddBooking(Booking model)
        {
            return _hotelDataAccess.AddBooking(model);
        }
    }
}
