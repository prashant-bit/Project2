using Microsoft.AspNetCore.Mvc;
using PROJECT_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT_3.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _Db;
        public StudentController(StudentContext Db)
        {
            _Db = Db;
        }
        public IActionResult StudentList()
        {
            var s = from a in _Db.tbl_Student
                    join b in _Db.tbl_Departments
                    on a.DepId equals b.Id
                    into Dep
                    from b in Dep.DefaultIfEmpty()

                    select new Student
                    {
                        Id = a.Id,
                        name = a.name,
                        DepId = a.DepId,

                        DepName = b == null ? "" : b.name
                    };
            var stdList = _Db.tbl_Student;
            return View(s);
        }

        public IActionResult Create(Student obj)
        {
            loadDDL();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent (Student obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.Id == 0)
                {
                    _Db.tbl_Student.Add(obj);
                    await _Db.SaveChangesAsync();
                   
                }
                else
                {
                    _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _Db.SaveChangesAsync();
                }

                return RedirectToAction("StudentList");

            }
            return View();
        }

        private void loadDDL()
        {
            List<Departments> depList = new List<Departments>();
            depList = _Db.tbl_Departments.ToList();

            ViewBag.DepList = _Db.tbl_Departments;
        }
    }
}
