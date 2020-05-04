using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIntranet.Controllers
{
    //[RequireHttps]
    public class PTSOController : Controller
    {
        // GET: PTSO
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string description, DateTime startDate, DateTime endDate)
        {
            return View();
        }
        
    }
}