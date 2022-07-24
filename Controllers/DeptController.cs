using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebApi.models;
using Microsoft.EntityFrameworkCore;
using DemoWebApi.ViewModels;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        _1026Context db = new _1026Context();
        [HttpGet]
        [Route("ListDept")]
        public IActionResult GetDept()
        {
            var data = from dep in db.Depts select new {Name=dep.Name, Id=dep.Id,Lcation=dep.Location };

            return Ok(data);
        }
        [HttpGet]
        [Route("ListDept/{id}")]
        public IActionResult GetDept(int? id) {

            if (id == null) {

                return BadRequest("Id cannot be null");
            }
            //Dept dept=db.Depts.Find(id);
            //var data1= db.Depts.Where(e=>e.id==id).Select(d=>new{Id=d.Id,Name=d.Id,Location=d.Location});

            var data = (from dept in db.Depts where dept.Id == id select new { Name = dept.Name, Id = dept.Id, Lcation = dept.Location }).FirstOrDefault();
            if (data == null) {
                return NotFound($"department with {id} is not found");
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("ListCity")]
        public IActionResult GetCity([FromQuery] string? city) {//........api/Dept/ListCity?city=mumbai

           /* if (city == null) {
                return NotFound($"department with {city} is not found");
            }use data.Count()==0 null collection will be created*/
            var data = db.Depts.Where(e => e.Location == city);
            return Ok(data);
        }


        [HttpPost]
        [Route("AddDept")]
        public IActionResult PostDept(Dept dept)
        {
            try
            {
                //db.Depts.Add(dept);
                //db.SaveChanges();

                db.Database.ExecuteSqlInterpolated($"adddepartment {dept.Id},{dept.Name},{dept.Location}");
            }
            catch(Exception e) {
                return BadRequest($"Something went wrong{e.Message}");
            }
            return Created("Record Succesfully Added",dept);
        }

        [HttpPut]
        [Route("EditDept/{id}")]
        public IActionResult PutDept(int id, Dept dept) {
            if(ModelState.IsValid){
                Dept dept1 = db.Depts.Find(id);
                dept1.Name = dept.Name;
                dept1.Location = dept.Location;
                db.SaveChanges();
                return Ok("Request Updated");
            }
            else return BadRequest("Unable edit record");
        }

        [HttpDelete]
        [Route("Deldept/{id}")]
        public IActionResult Deletedept(int id) {

            try
            {
                Dept dept1 = db.Depts.Find(id);
                db.Depts.Remove(dept1);
                db.SaveChanges();
                
            }
            catch(Exception ex){
                return BadRequest($"Problem with delete {ex.Message}");
            }
            return Ok("Dept deleted");

        }


        [HttpGet]
        [Route("ShowEmp")]
        public IActionResult GetEmp() {

            var data = db.EmpDepts.FromSqlInterpolated<Empdept>($"ShowEmp");
            return Ok(data);
        }
    }
}
