using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Entities
{
    public partial class Person
    {
        public Person()
        {
            Booking = new HashSet<Booking>();
        }

        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNo { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
