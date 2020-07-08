using System.ComponentModel.DataAnnotations;
using System;
using Hotel.Models;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class RoomListModel
    {
       public IEnumerable<int> RoomType { get; set; }
       public IEnumerable<string> City { get; set; }
    }
}