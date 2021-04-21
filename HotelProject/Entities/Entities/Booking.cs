using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            BookingTime = DateTime.Now;
            IsDeleted = false;
        }
        public long BookingId { get; set; }
        public long PersonId { get; set; }
        public long RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfPerson { get; set; }
        public DateTime? BookingTime { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual Room Room { get; set; }
    }
}
