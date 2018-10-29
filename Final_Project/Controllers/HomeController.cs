using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {

        ITestCoordinator TestService;
        public HomeController(ITestCoordinator TestServ)
        {
            TestService = TestServ;
        }
        static int Di_id;
        static int Cl_id;
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("Index", "UserTests");
        }
    

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("Index", "UserTests");
        }

        public ActionResult Contact(int D_id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Di_id = D_id;
                return View();
            }
            else return RedirectToAction("Index", "UserTests");
            
        }
        public RedirectToRouteResult Result(int C_id)
        {
            Cl_id = C_id;
            var testId=TestService.GetTest(Di_id, C_id);
            return RedirectToAction("Index", "Test", new { id = testId });
        }
    }
}