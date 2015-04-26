using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class AFController : Controller
    {
        // GET: AF
        public ActionResult Index()
        {
            return View();
        }

        [HandleError(ExceptionType = typeof(ArgumentException), View = "Error.Argument")]
        public ActionResult MademeWrong(string type="")
        {
            if (type == "1")
            {
                throw new ArgumentException("ArgumentError");
            }
            else
            {
                throw new Exception("error");
            }

            return View();
        }

    }
}