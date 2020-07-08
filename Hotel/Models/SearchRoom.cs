using System.ComponentModel.DataAnnotations;
using System;
using Hotel.Models;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class SearchRoom
    {
       //public string City { get; set; } 
      // public string RoomType { get; set; }
       public IEnumerable<RoomType> RoomType { get; set; }
       public IEnumerable<string> City { get; set; }
        
    }
}