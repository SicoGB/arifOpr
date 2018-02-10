using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using opr.Models;
using Microsoft.AspNet.Identity;
namespace opr.Controllers
{
    public class SchoolController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            Models.SchoolDbEntities dbs = new SchoolDbEntities();
            List<Models.School> model = new List<School>();
            model = dbs.Schools.ToList();
            return View(model);
            
        }
        public ActionResult AddSchool()
        {
            ViewBag.ids = User.Identity.GetUserId();
            return View();
        }
        [HttpPost]
        public ActionResult AddSchool(opr.Models.School sch)
        {

            Models.SchoolDbEntities db = new SchoolDbEntities();
            db.Schools.Add(sch);
            db.SaveChanges();
            return View();
           
        }
        [HttpPost]
        public ActionResult EditSchool(opr.Models.School schl)
        {
            Models.SchoolDbEntities sub = new SchoolDbEntities();

            sub.Entry(schl).State = System.Data.Entity.EntityState.Modified;
            try
            {
                sub.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

            }
            ModelState.AddModelError("", "Unable to save changes made to the entity.");
            return View(schl);

        }
        public ActionResult Delete(int id)
        {
            Models.SchoolDbEntities sub = new SchoolDbEntities();
            School student_table = sub.Schools.Find(id);
            sub.Schools.Remove(student_table);
            sub.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}