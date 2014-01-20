using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;
using System.Net.Mime;

using SMSOverEmail;

namespace Example.Controllers
{
    public class PhoneViewModel
    {
        [Required]
        public string Phone { get; set; }

        public Carrier Carrier { get; set; }

        [Required]
        public string Subject { get;set;}

        [Required]
        public string Body { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Success = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(PhoneViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Send the message
                MailMessage message = new MailMessage();

                message.AddToSMS(model.Phone, model.Carrier);
                message.Subject = model.Subject;
                message.SetBody(model.Body);

                message.SendMailSmtp();

                // Tell the user about it
                string email = CarrierInfo.GetSMSAddress(model.Phone, model.Carrier);
                string userMsg = string.Format("Successfully sent e-mail to {0}", email);
                ViewBag.Success = userMsg;
            }

            return View();
        }
    }
}