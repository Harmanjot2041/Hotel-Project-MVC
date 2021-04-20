using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelProject.Models;
using Microsoft.AspNetCore.Mvc;
using HotelBussinessLayer;
using Entities.Entities;
using Entities.Custom;

namespace HotelProject.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelComponent _hotelComponent;
        public HotelController(IHotelComponent hotelComponent)
        {
            _hotelComponent = hotelComponent;
        }
        public IActionResult Home()
        {
            DateCheckModel temp = new DateCheckModel();
            temp.CheckInDate = DateTime.Now;
            temp.CheckOutDate = DateTime.Now;
            
            return View(temp);
        }
        [HttpPost]
        public IActionResult Home(DateCheckModel  model)
        {
            ViewBag.Error = "";
            if (model.CheckInDate.Date < DateTime.Now.Date || model.CheckOutDate.Date < DateTime.Now.Date)
            {
                ViewBag.Error = "Please Choose date from today";
                return View(model);
            }
            else if (model.CheckOutDate <= model.CheckInDate)
            {
                ViewBag.Error = "Checkout Date Should Greater then CheckIn";
                return View(model);
            }
            else if (model.CheckOutDate.Year == model.CheckInDate.Year)
            {
                if ((model.CheckOutDate.DayOfYear - model.CheckInDate.DayOfYear) > 3)
                {
                    ViewBag.Error = "You can Stay for only 3 days";
                    return View(model);
                }

            }
            if(model.Person <1 || model.Rooms < 1||model.Person < model.Rooms)
            {
                ViewBag.Error = "Please Enter Valid No of  Person And Rooms";
                return View(model);
            }
            return RedirectToAction("SearchResult",model);
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Restaurant()
        {
            return View();
        }
        public IActionResult Testimonials()
        {
            return View(_hotelComponent.GetTestimonials());
        }
        public IActionResult Booking()
        {
            return View();
        }
        public IActionResult SearchResult(DateCheckModel model)
        {
            TempData["CheckInDate"] = model.CheckInDate;
            TempData["CheckOutDate"] = model.CheckOutDate;
            TempData["NoOfPerson"] = model.Person;
            TempData["NoOfRooms"] = model.Rooms;
            ViewBag.CheckInDate = model.CheckInDate.ToString("MM/dd/yyyy");
            ViewBag.CheckOutDate = model.CheckOutDate.ToString("MM/dd/yyyy");
            ViewBag.Person = model.Person;
            ViewBag.Rooms = model.Rooms;
            return View(_hotelComponent.SearchedRooms(model));
        }
        public IActionResult AddTestimonials()
        {
            return View(new Testimonials());
        }
        [HttpPost]
        public IActionResult AddTestimonials(Testimonials model)
        {
            int result =_hotelComponent.AddTestimonials(model);
            if (result == 1)
                return RedirectToAction("Testimonials");

            return View();
        }
        
        public IActionResult RoomBooking(string roomType)
        {
            try
            {
                ViewBag.Nights = Convert.ToDateTime(TempData["CheckOutDate"]).DayOfYear - Convert.ToDateTime(TempData["CheckInDate"]).DayOfYear;
                ViewBag.CheckInDate = Convert.ToDateTime(TempData["CheckInDate"]).ToString("MM/dd/yyyy");
                ViewBag.CheckOutDate = Convert.ToDateTime(TempData["CheckOutDate"]).ToString("MM/dd/yyyy");
                ViewBag.Rooms = Convert.ToInt32(TempData["NoOfRooms"]);
                CustomBooking customBooking = new CustomBooking();
                CustomRoomCheck customRoomCheck = new CustomRoomCheck();
                customRoomCheck.CheckInDate = Convert.ToDateTime(TempData["CheckInDate"]);
                customRoomCheck.CheckOutDate = Convert.ToDateTime(TempData["CheckOutDate"]);
                customRoomCheck.RoomType = roomType;
                customBooking.Room = _hotelComponent.GetAvailableRoomByType(customRoomCheck);
                customBooking.Person = new Person();
                return View(customBooking);
            }
            catch(Exception e)
            {
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult RoomBooking(CustomBooking model)
        {
            DateTime checkInDate = Convert.ToDateTime(TempData["CheckInDate"]);
            int noOfPerson = Convert.ToInt32(TempData["NoOfPerson"]);
            int noOfRooms = Convert.ToInt32(TempData["NoOfRooms"]); 
            DateTime checkOutDate = Convert.ToDateTime(TempData["CheckOutDate"]);
            long personId = _hotelComponent.AddPerson(model.Person);
            Booking booking = new Booking();
            booking.RoomId = model.Room.RoomId;
            booking.CheckInDate = checkInDate;
            booking.CheckOutDate = checkOutDate;
            booking.PersonId = personId;
            booking.NoOfPerson = noOfPerson;
            int result = _hotelComponent.AddBooking(booking);
            if (result == 1)
                ViewBag.message = "Booking Done Succesfully";
            else
                ViewBag.message = "This Room is ALready booked";
            
            return View(model);
        }

    }
}