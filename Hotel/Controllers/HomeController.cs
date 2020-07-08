using System.Data;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly WdaContext _context;

        private readonly ILogger<HomeController> _logger;

       
        public HomeController(WdaContext context)
        {
            this._context = context;
        }

         public IActionResult Index()
        {
            
           return View();
        }
        public IActionResult Landing()
        {
             IEnumerable<string> allCities = _context.Room.Select(room => room.City ).Distinct();
            IEnumerable<int> allRoomTypes = _context.Room.Select(room => room.RoomType ).Distinct();
             var model =  new Hotel.Models.RoomListModel
            {
                
                City = allCities,
                RoomType =allRoomTypes
                

            };
                return View(model);
        }

        
        public IActionResult RoomList(string city,int select,string checkin,string checkout,int countofguests,int lowprice,int highprice)
        {    
            if(countofguests == 0 ){
                IQueryable<Hotel.Models.Room> result = _context.Room;
                if (!string.IsNullOrEmpty(city))
                {
                    if( city == "Any")
                    {
                        ViewData["city"] = city;
                        result = _context.Room;
                    }
                    else
                    {
                    ViewData["city"] = city;
                    result = result.Where(room => room.City == city);
                    }
                }
                if (select != 0)
                {
                    if( select != 1 && select !=2 && select != 3 && select !=4 )
                    {
                        ViewData["city"] = city;
                        result = _context.Room;
                    }
                    result = result.Where(room => room.RoomType == select);
                }
                ViewBag.cityoption = city;
                ViewBag.roomtypeoption = select;
                ViewBag.checkinoption = checkin;
                ViewBag.checkoutoption = checkout;
            
                    return View(result );  
            }
            else{
                IQueryable<Hotel.Models.Room> result = _context.Room;
                if (!string.IsNullOrEmpty(city))
                {
                    ViewData["city"] = city;
                    result = result.Where(room => room.City == city);
                }
                if (select != 0)
                {
                    result = result.Where(room => room.RoomType == select);
                }
                if (countofguests != 0)
                {
                    result = result.Where(room => room.CountOfGuests == countofguests);
                }
                    result = result.Where(room => room.Price >= lowprice && room.Price <= highprice);

                ViewBag.cityoption = city;
                ViewBag.roomtypeoption = select;
                ViewBag.checkinoption = checkin;
                ViewBag.checkoutoption = checkout;
                ViewBag.countofguests = countofguests;
                ViewBag.lowprice = lowprice;
                ViewBag.highprice = highprice;
           
                return View(result );  
            }
        }   
        
        [HttpGet]
        public IActionResult Room(string checkin,string checkout,int roomId)
        {
          
            RoomDetailsModel roomDetails = new RoomDetailsModel
            {
                Details = _context.Room.Where(room => room.RoomId == roomId).FirstOrDefault(),
                Reviews = _context.Reviews.Where(review => review.RoomId == roomId).ToList()
            };

            var model =  new Hotel.Models.BookingModel
            {
                RoomId = roomId,
                RoomDetails = roomDetails,
                CheckInDate = checkin,
                CheckOutDate = checkout,
            };

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Book( string checkin, string checkout, int roomId)
        {
            if (ModelState.IsValid)
            {
                DateTime parsedCheckInDate;
                DateTime parsedCheckOutDate;
                bool checkInDateValid = true, checkOutDateValid = true;
                if (!DateTime.TryParse(checkin, out parsedCheckInDate)) 
                {
                    ModelState.AddModelError("CheckInDate", "Invalid check in date format");
                    checkInDateValid = false;
                }
                if (!DateTime.TryParse(checkout, out parsedCheckOutDate)) 
                {
                    ModelState.AddModelError("CheckOutDate", "Invalid check out date format");
                    checkOutDateValid = false;
                }
                if (checkInDateValid && checkOutDateValid && parsedCheckInDate > parsedCheckOutDate)
                {
                    ModelState.AddModelError("", "Check in date is too late");
                    checkInDateValid = false;
                }
                if (checkInDateValid && checkOutDateValid)
                {
                    
                    var entry = new Bookings
                    {
                        RoomId = roomId,
                        UserId = 1,
                        CheckInDate = checkin,
                        CheckOutDate = checkout,
                    };
                    _context.Bookings.Add(entry);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult Room(int starreview, string comment, int roomid,string checkin, string checkout)
        {
            var entry = new Reviews
                {
                    Rate = starreview,
                    Text = comment,
                    RoomId = roomid,
                    UserId = 1
                };
                _context.Reviews.Add(entry);
                _context.SaveChanges();


            return RedirectToAction("Room", "Home", new { checkin = checkin, checkout = checkout, roomid });
        }
        public IActionResult Favorite(int starreview, string comment, int roomid,string checkin, string checkout)
        {
            var entry = new Favorites
                {
                    
                    Status = 1,
                    RoomId = roomid,
                    UserId = 1
                };
                _context.Favorites.Add(entry);
                _context.SaveChanges();

            return RedirectToAction("Room", "Home", new { checkin = checkin, checkout = checkout, roomid });
        }
        public IActionResult Profile()
        {
            
            IEnumerable<Favorites> Likes  = _context.Favorites.Where(room => room.UserId == 1).ToList();

            IEnumerable<Bookings> Bookid  = _context.Bookings.Where(room => room.UserId == 1).ToList();
            
            IEnumerable<Bookings> Book  = _context.Bookings.Where(room => room.UserId == 1).ToList();

            IEnumerable<Reviews> Reviews  = _context.Reviews.Where(room => room.UserId == 1).ToList();

            
             var model =  new Hotel.Models.ProfileModel
            {           
                Favorite = Likes,
                Bookingid=Book,
                 
                 Review  = Reviews,
                 Details = _context.Room.Select(room => room).ToList()
                
            };
                return View(model);
        }
            
        
        
       

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
