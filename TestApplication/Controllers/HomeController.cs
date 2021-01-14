using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        DatabaseTestEntities db = new DatabaseTestEntities();
        public ActionResult Index()
        {
            List<Ofice> ofices = db.Ofice.ToList();
            ViewBag.P1 = new SelectList(ofices.ToList(), "Id","Name");
            return View();
        }
        
        public ActionResult GetTimeSlots(int officeId , DateTime DateAndTime)
        {
            DateTime date = DateAndTime;
            date += new TimeSpan(23, 59, 59);
            List<TimeSlot> timeslot = db.TimeSlot.Where(x => x.OfficeId == officeId && (x.BeginTime >= DateAndTime && x.EndTime <= date)).ToList();
            return PartialView(timeslot);
        }
        public ActionResult Arrange(int officeId, string DateAndTime)
        {
            DateTime begindate = Convert.ToDateTime(DateAndTime);
            DateTime enddate = begindate + new TimeSpan(0,30,0);
            db.TimeSlot.Add(new TimeSlot { BeginTime = begindate, EndTime = enddate, IsBusy = true, OfficeId = officeId });
            db.SaveChanges();
           
            return Content("Вы успешно записались на " + DateAndTime);
        }
        public ActionResult Cancel(int officeId, string DateAndTime)
        {
            DateTime begindate = Convert.ToDateTime(DateAndTime);
            DateTime enddate = begindate + new TimeSpan(0, 30, 0);
            var Cansel = db.TimeSlot.Where(x => x.OfficeId == officeId && x.BeginTime == begindate).ToList().FirstOrDefault();
            db.TimeSlot.Remove(Cansel);
            db.SaveChanges();

            return Content("Вы успешно отменили запись на " + DateAndTime);
        }
    }
}