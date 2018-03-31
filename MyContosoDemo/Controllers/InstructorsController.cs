using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

using MyContosoDemo.ViewModels;

namespace MyContosoDemo.Controllers
{
    public class InstructorsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Instructors
        public ActionResult Index(int? Id)
        {

            if(Id.HasValue)
            {
                List<InstructorCoursesVM> instructorcoursesVM = new List<InstructorCoursesVM>();

                ViewBag.InstructorCourses = db.Instructors.Include(i => i.Courses).Where(i=>i.ID==Id).ToList();
            }

            var Instructors = db.Instructors.Include(i => i.OfficeAssignment)
                .Include(i=>i.Courses).ToList();

            
        

            return View(Instructors.ToList());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            //ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location");

            ViewBag.Courses= db.Courses.ToList();






            return View();
        }

        public bool GetModelErrors()
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                
                if(modelErrors.Any())
                {
                    return false;
                }
            }
            return true;
        }



        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate,OfficeAssignment")] Instructor instructor,string[] selectedCourses)
        {
            GetModelErrors();

            if (ModelState.IsValid)
            {
                if (selectedCourses != null)
                {
                    instructor.Courses = new List<Course>();

                    foreach (var c in selectedCourses)
                    {
                        var course = db.Courses.Find(int.Parse(c));

                        instructor.Courses.Add(course);
                    }
                    
                }

                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }


         

            // GET: Instructors/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

             

            Instructor instructor = db.Instructors
                                    .Include(i => i.Courses)
                                    .Include(i => i.OfficeAssignment)
                                    .Single(i => i.ID == id);

                
            if (instructor == null)
            {
                return HttpNotFound();
            }


            var allCouses = db.Courses.ToList();

            List<InstructorEditVM> CouseVm = new List<InstructorEditVM>();

            var instructorCouses= new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
            foreach (var C in allCouses)
            {
                InstructorEditVM Vm = new InstructorEditVM()
                {
                    CourseId=C.CourseID,
                    Title=C.Title,
                    IsChecked=(instructorCouses.Contains(C.CourseID))
                };

                CouseVm.Add(Vm);
            }

            ViewBag.InstuctorCouses = CouseVm;



            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {




                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
