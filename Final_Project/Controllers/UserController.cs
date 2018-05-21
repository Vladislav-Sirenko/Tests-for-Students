using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;
using System.Web.Mvc;
using BLL.Interfaces;
using AutoMapper;

namespace Final_Project.Controllers
{
    public class UserController : Controller
    {
        IUserManageService UserService;
        public UserController(IUserManageService userserv)
        {
            UserService = userserv;
        }
        // GET: User
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                IEnumerable<ClientProfileDTO> userDTO = UserService.GetUsers();
                return View(userDTO);
            }
            else return RedirectToAction("Index", "UserTests");
        }
    }
}