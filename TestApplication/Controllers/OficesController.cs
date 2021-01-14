using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestApplication;

namespace TestApplication.Controllers
{
    public class OficesController : ApiController
    {
        private DatabaseTestEntities db = new DatabaseTestEntities();

      
        public IQueryable<Ofice> GetOfice()
        {
            return db.Ofice;
        }
        public IQueryable<TimeSlot> GetTimeSlots(int officeId, DateTime date)
        {
            DateTime DateAndTime = date;
            DateAndTime += new TimeSpan(23, 59, 59);
            return db.TimeSlot.Where(x => x.OfficeId == officeId && (x.BeginTime >= date && x.EndTime <= DateAndTime));
        }

    }
}