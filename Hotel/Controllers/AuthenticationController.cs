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
using Microsoft.AspNetCore.Identity;

namespace Hotel.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController(UserManager<User> userManager,
                                        SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public IActionResult Index()
        {
            return View();
        }
                [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
