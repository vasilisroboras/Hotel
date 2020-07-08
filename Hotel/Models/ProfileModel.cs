using System.ComponentModel.DataAnnotations;
using System;
using Hotel.Models;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class ProfileModel
    {
       public IEnumerable<Favorites> Favorite { get; set; }

        public IEnumerable<Bookings> Bookingid { get; set; }

        public IEnumerable<Reviews> Review { get; set; }

        public IEnumerable<Room> Details { get; set; }

    }
}