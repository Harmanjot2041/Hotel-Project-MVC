using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Custom
{
    public class DateCheckModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Person { get; set; } 
        public int Rooms { get; set; }
    }
}
