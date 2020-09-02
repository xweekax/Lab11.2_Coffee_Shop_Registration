using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Net.Mail;


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

        public ActionResult Welcome(string firstname, string lastname, string password, string emailaddress, string phonenumber)
        {            
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var phoneNumberValidation = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

            bool isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

            bool emailIsValid;

            bool phoneNumberIsValid = phoneNumberValidation.IsMatch(phonenumber);
            
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
                ViewBag.Message = $"Welcome {firstname} {lastname}!";
                /*
                string to = emailaddress;
                string from = "xweekax@gmail.com.com";
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Big Bean Coffee!";
                message.Body = $"Welcome to Big Bean Coffee {firstname} {lastname}! Please verify the following:" +
                    $"phone number: {phonenumber}" +
                    $"password: {password}";
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = true;

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                        ex.ToString());
                }
                */
                return View();
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