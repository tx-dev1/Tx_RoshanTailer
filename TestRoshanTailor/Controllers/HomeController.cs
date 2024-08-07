﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        SqlConnection con;
        String constring;
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
        public JsonResult Index(RegisterViewModel model)
        {
            var output = string.Empty;
            var result = RegisterUser(model);
            if (result > 0)
            {
                // Registration successful, redirect to login or home page
                //return RedirectToAction("Contact", "Home");
                output = "Registration Successfull.";
            }
            else if (result == -1)
            {
                // ModelState.AddModelError(string.Empty, "Username already exists.");
                output = "Username already exists.try different one.";
                //return Json(output, JsonRequestBehavior.AllowGet);
            }
            else if (result == -2)
            {
                output = "Email already exists. try different one.";
                //ModelState.AddModelError(string.Empty, "Email already exists.");
                //return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                output = "Internal Server error. please try again.";
                //ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                //return Json(output, JsonRequestBehavior.AllowGet);
            }

            return Json(output, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Login(LoginViewModel model)
        {
            var output = string.Empty;
            var result = LoginUser(model);
            if (result > 0)
            {
                // Registration successful, redirect to login or home page
                //return RedirectToAction("Contact", "Home");
                output = "Login Successfull.";
            }            
            else
            {
                output = "Invalid username and password";
                //ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                //return Json(output, JsonRequestBehavior.AllowGet);
            }

            return Json(output, JsonRequestBehavior.AllowGet);

        }
        private int RegisterUser(RegisterViewModel model)
        {
            int result = 0;
            try
            {
                SqlConnection con = new SqlConnection();
                constring = ConfigurationManager.ConnectionStrings["rosharxk_Entities"].ConnectionString;

                using (rosharxk_Entities rx = new rosharxk_Entities())
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("rosharxk_Entities")))
                    {
                        con = new SqlConnection(constring);
                        using (var command = new SqlCommand("SP_UserRegistration", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Username", model.Username);
                            command.Parameters.AddWithValue("@Password", model.Password);
                            command.Parameters.AddWithValue("@Email", model.Email);
                            command.Connection = con;
                            con.Open();
                            result = Convert.ToInt32(command.ExecuteScalar());
                            con.Close();
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
        }


        private int LoginUser(LoginViewModel model)
        {
            int result = 0;
            try
            {
                SqlConnection con = new SqlConnection();
                constring = ConfigurationManager.ConnectionStrings["rosharxk_Entities"].ConnectionString;

                using (rosharxk_Entities rx = new rosharxk_Entities())
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("rosharxk_Entities")))
                    {
                        con = new SqlConnection(constring);
                        using (var command = new SqlCommand("SP_UserLogin", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Username", model.Username);
                            command.Parameters.AddWithValue("@Password", model.Password);                            
                            command.Connection = con;
                            con.Open();
                            result = Convert.ToInt32(command.ExecuteScalar());
                            con.Close();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
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