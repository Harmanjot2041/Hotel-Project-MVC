using Entities.Custom;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelDataAccessLayer
{
    public class HotelDataAccess:IHotelDataAccess
    {
        private readonly HotelDbContext _context;
        public HotelDataAccess(HotelDbContext context)
        {
            _context = context;
        }

        public int AddTestimonials(Testimonials model)
        {
            _context.Testimonials.Add(model);
            return _context.SaveChanges();
        }

        public List<CustomRoom> SearchedRooms(DateCheckModel model)
         {
            List<Booking> bookings = _context.Booking.ToList();
            List<Room> availableRooms;
            if (bookings.Count() != 0)
            { 
                  availableRooms = (from room in _context.Room.ToList() where !(
                    from booking in bookings
                    where (booking.CheckInDate <= model.CheckInDate && booking.CheckOutDate > model.CheckInDate) ||
                   (booking.CheckInDate < model.CheckOutDate && booking.CheckOutDate >= model.CheckOutDate) ||
                   (booking.CheckInDate >= model.CheckInDate && booking.CheckOutDate <= model.CheckOutDate)
                    select booking.RoomId).Contains(room.RoomId) select room
                    ).ToList();
            }
            else
            {
                availableRooms = _context.Room.ToList();
            }
            List<CustomRoom> temp = (from item in availableRooms group item by
                 new { item.Price, item.Bed, item.Capacity, item.RoomType, item.RoomImage, item.Discription } into newItem
                select new CustomRoom(){ 
                    Price = newItem.Key.Price,
                    Bed = newItem.Key.Bed,
                    Capacity = newItem.Key.Capacity,
                    RoomType = newItem.Key.RoomType,
                    RoomImage = newItem.Key.RoomImage,
                    Discription = newItem.Key.Discription,
                    NoOfRooms = newItem.Count() 
                }).ToList();
            if (model.Rooms == 1)
            {
                temp = (from item in temp where item.Capacity >= model.Person select item).ToList();
            }
            else
            {
                temp = (from item in temp where item.NoOfRooms >= model.Rooms && item.Capacity < model.Person&&(item.Capacity*item.NoOfRooms) >= model.Person select item).ToList();
            }
            return temp;
        }

        public List<Testimonials> GetTestimonials()
        {
            return _context.Testimonials.ToList();
        }
        public Room GetAvailableRoomByType(CustomRoomCheck model)
        {
            List<Booking> bookings = _context.Booking.ToList();
            List<Room> availableRooms;
            if (bookings.Count() != 0)
            { 
                  availableRooms = (from room in _context.Room.ToList() where !(
                    from booking in bookings
                    where (booking.CheckInDate <= model.CheckInDate && booking.CheckOutDate > model.CheckInDate) ||
                   (booking.CheckInDate < model.CheckOutDate && booking.CheckOutDate >= model.CheckOutDate) ||
                   (booking.CheckInDate >= model.CheckInDate && booking.CheckOutDate <= model.CheckOutDate)
                    select booking.RoomId).Contains(room.RoomId) select room
                    ).ToList();
            }
            else
            {
                availableRooms = _context.Room.ToList();
            }

            return availableRooms.FirstOrDefault(x => x.RoomType == model.RoomType);
        }
        public long AddPerson(Person model)
        {
            Person check = _context.Person.FirstOrDefault(x => x.Email == model.Email);
            if (check != null)
                return check.PersonId;
            _context.Person.Add(model);
            _context.SaveChanges();
            Person person = _context.Person.FirstOrDefault(x => x.Email == model.Email);
            return person.PersonId;
        }
        public int AddBooking(Booking model)
        {
           List<Booking> bookings = _context.Booking.ToList();
           
            if (bookings.Count() != 0)
            {
               List<Booking> notAvailable = _context.Booking.Where(booking => booking.RoomId == model.RoomId &&
                ((booking.CheckInDate <= model.CheckInDate && booking.CheckOutDate > model.CheckInDate) ||
                (booking.CheckInDate < model.CheckOutDate && booking.CheckOutDate >= model.CheckOutDate) ||
                  (booking.CheckInDate >= model.CheckInDate && booking.CheckOutDate <= model.CheckOutDate))).ToList();
                  
                if(notAvailable.Count() >0)
                {
                    return 2;
                }
            }
            _context.Booking.Add(model);
            return _context.SaveChanges();
        }
    }
}
