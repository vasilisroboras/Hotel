using System.ComponentModel.DataAnnotations;
using System;
using Hotel.Models;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class ReviewModel
    {
        public int Rate { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}