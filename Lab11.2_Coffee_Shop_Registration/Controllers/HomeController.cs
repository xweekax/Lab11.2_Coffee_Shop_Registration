using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Lab11._2_Coffee_Shop_Registration.Models;


namespace Lab11._2_Coffee_Shop_Registration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            ViewBag.Message = "Your registration page.";

            return View();
        }
        /*
        public ActionResult OrderPage(string firstname, string stAddress, string city, string zipCode, string selectTime)
        {
            WebUser orderPage = new WebUser()
            {
                FirstName = firstname,
                StAddress = stAddress,
                City = city,
                ZipCode = zipCode,
                SelectTime = selectTime

            };
            return View(orderPage);
        }
        */
        [HttpPost]
        public ActionResult OrderConfirmation(string drink, string size, string getDrink, string firstname, string stAddress, string city, string zipCode, string selectTime)
        {
            WebUser drinkOrder = new WebUser()
            {
                Drink = drink,
                Size = size,
                GetDrink = getDrink,
                FirstName = firstname,
                StAddress = stAddress,
                City = city,
                ZipCode = zipCode,
                SelectTime = selectTime
            };

            Random orderNumber = new Random();
            ViewBag.OrderNumber = orderNumber.Next();

            return View(drinkOrder);
        }

        [HttpPost] //using post means we cannot "get" the page
        public ActionResult OrderPage(string firstname, string lastname, string password, string emailaddress, string phonenumber, string username, string stAddress, string city, string zipCode, string selectTime)
        {            
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var phoneNumberValidation = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

            bool isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
            bool emailIsValid;
            bool phoneNumberIsValid = phoneNumberValidation.IsMatch(phonenumber);

            WebUser user = new WebUser()
            {
                Username = username,
                Email = emailaddress,
                FirstName = firstname,
                LastName = lastname,
                PhoneNumber = phonenumber,
                Password = password,
                City = city,
                ZipCode = zipCode,
                SelectTime = selectTime
            };
            
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                emailIsValid=true;
            }
            catch (FormatException)
            {
                emailIsValid=false;
            }
            
            if (isValidated && emailIsValid && phoneNumberIsValid)
            {                
                return View(user);
            }
            else if(!isValidated)
            {
                return Content ($"<h2>Password does not meet reqirements, Please click <a href=/home/Registration>Here</a> to try again</h2>" +
                    $"Password must contain:<br/><ul><li>At Least 1 Capital Letter</li><li>At Least 1 Number</li><li>At Least 1 Special Character</li><li>At Least 8 Character Length</li></ul>");
                
            }
            else if(!emailIsValid)
            {
                return Content($"<h2>Email does not meet reqirements, Please click <a href=/home/Registration>Here</a> to try again</h2>");

            }
            else if(!phoneNumberIsValid)
            {
                return Content($"<h2>Phone Number does not meet reqirements, Please click <a href=/home/Registration>Here</a> to try again</h2>");

            }
            else
            {
                return Content($"<h2>Sopmething went wrong, Please click <a href=/home/Registration>Here</a> to try again</h2>");

            }
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}