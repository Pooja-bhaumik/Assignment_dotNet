using Data;
using Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        EmployeeDbContext _context;


        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpPost]
        public IActionResult Post([FromBody] tblEmployees _oTblEmployees)
        {
            try
            {
                string response = "";
                if (_oTblEmployees != null)
                {
                    _context.tblEmployees.Add(_oTblEmployees);
                    _context.SaveChanges();
                    response = "Employee Added Successfully";
                    return Ok(response);
                }
                else
                {
                    response = "Something went wrong";
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [HttpGet]
        public IActionResult Get(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.tblEmployees.OrderByDescending(x => x.ID));
                else
                    return Ok(_context.tblEmployees
                        .Where(x => x.FirstName.Contains(search) || x.EmployeeCode.Contains(search))
                        .OrderByDescending(x => x.ID));

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }

        }
        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var Employee = _context.tblEmployees.FirstOrDefault(x => x.ID == id);
                if (Employee != null)
                {
                    return Ok(Employee);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }

        }
        [HttpPut]
        public IActionResult Put([FromBody] tblEmployees _oTblEmployees)
        {
            try
            {
                string response = "";
                if (_oTblEmployees != null)
                {
                    _context.tblEmployees.Update(_oTblEmployees);
                    _context.SaveChanges();
                    response = "Employee Updated Successfully";
                    return Ok(response);
                }
                else
                {
                    response = "Something went wrong";
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ExistingEmployee = _context.tblEmployees.Where(x => x.ID == id).FirstOrDefault<tblEmployees>();
                if (ExistingEmployee != null)
                {
                    _context.tblEmployees.Remove(ExistingEmployee);
                    _context.SaveChanges();
                    return Ok("Record Deleted");
                }
                else
                {
                    return Ok("Not Found");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
