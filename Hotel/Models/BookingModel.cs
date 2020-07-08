using System.ComponentModel.DataAnnotations;
using System;
using Hotel.Models;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class BookingModel
    {
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int RoomId { get; set; }

        //public IEnumerable<RoomDetailsModel> RoomDetails { get; set; }
        public RoomDetailsModel RoomDetails { get; set; }
    }
}