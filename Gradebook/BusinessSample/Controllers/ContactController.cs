﻿using Application.Data;
using BusinessSample.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSample.Controllers
{
    public class ContactController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
    }
}
