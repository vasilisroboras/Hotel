using System.Collections.Generic;

namespace Hotel.Models
{
    public class RoomDetailsModel
    {
        public Room Details { get; set; }
        public IEnumerable<Reviews> Reviews { get; set; }
    }
}