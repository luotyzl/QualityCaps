using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QualityCaps.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            MailAddress toAddress = new MailAddress("luotyzl@gmail.com");
            MailAddress fromAddress = new MailAddress("wad@unitec.com");
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = "Error Report";
            message.Body = "An error has happend, please check the error log.";
            SmtpClient mailClient = new SmtpClient();

            try
            {
                //mailClient.Host = "localhost"; //not working anymore
                mailClient.Host = "mail.unitec.ac.nz";
                mailClient.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                Response.Write("Email is not sent due to system error: " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }

            return View();
        }
    }
}