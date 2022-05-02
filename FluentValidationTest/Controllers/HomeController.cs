using FluentValidation;
using FluentValidation.Results;
using FluentValidationTest.Models;
using FluentValidationTest.Validators;
using FormHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [FormValidator]
        public IActionResult Save(Employee employee)
        {
            EmployeeValidator validator = new EmployeeValidator();
            ValidationResult result = validator.Validate(employee);
            if (!result.IsValid)
            {
                return FormResult.CreateErrorResult("Invalid Data.");
            }

            return FormResult.CreateSuccessResult("Valid Data.");
        }

        public IActionResult Privacy()
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
