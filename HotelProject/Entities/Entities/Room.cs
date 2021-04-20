using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Entities
{
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
        }

        public long RoomId { get; set; }
        public int RoomNo { get; set; }
        public long Price { get; set; }
        public string Bed { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; }
        public string RoomImage { get; set; }
        public string Discription { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
