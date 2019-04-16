using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class GevechtController : Controller
    {
        public IActionResult Aanval(GevechtDetailViewModel vm)
        {
            if (vm.SpelerAanZet == true)
            {

            }
            else
            {

            }
            return RedirectToAction();
        }
    }
}