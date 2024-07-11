using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TestRoshanTailor.Models;

namespace TestRoshanTailor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public HomeController()
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //      [HttpPost]
        //      public async Task<IActionResult> Index(RegisterViewModel model)
        //      {
        //	if (ModelState.IsValid)
        //	{
        //		var result = await RegisterUser(model);
        //		if (result > 0)
        //		{
        //			// Registration successful, redirect to login or home page
        //			return (IActionResult)RedirectToAction("Contact", "Home");
        //		}
        //		else if (result == -1)
        //		{
        //			ModelState.AddModelError(string.Empty, "Username already exists.");
        //		}
        //		else if (result == -2)
        //		{
        //			ModelState.AddModelError(string.Empty, "Email already exists.");
        //		}
        //		else
        //		{
        //			ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
        //		}
        //	}

        //	return (IActionResult)View(model);
        //}



        [HttpPost]
        public ActionResult Index(RegisterViewModel model)
        {

            var result = RegisterUser(model);
            if (result.Result > 0)
            {
                // Registration successful, redirect to login or home page
                return RedirectToAction("Contact", "Home");
            }
            else if (result.Result == -1)
            {
                ModelState.AddModelError(string.Empty, "Username already exists.");
            }
            else if (result.Result == -2)
            {
                ModelState.AddModelError(string.Empty, "Email already exists.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            }

            return View(model);

        }
        private async Task<int> RegisterUser(RegisterViewModel model)
        {
            int result = 0;
            using (rosharxk_Entities rx = new rosharxk_Entities())
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("rosharxk_Entities")))
                {
                    using (var command = new SqlCommand("SP_UserRegistration", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", model.Username);
                        command.Parameters.AddWithValue("@Password", model.Password);
                        command.Parameters.AddWithValue("@Email", model.Email);

                        await connection.OpenAsync();
                        result = (int)await command.ExecuteScalarAsync();
                    }
                }

                return result;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a Test Word";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}