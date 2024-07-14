using Contracts;
using Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeContract _iemployee;
        public EmployeeController(IEmployeeContract iemployeecontract)
        {
            _iemployee = iemployeecontract;
        }
        [Authorize(Policy = "RequireAdminOrManager")]
        public ActionResult EmployeeList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("EmployeeList" + ex.Message);
            }
        }
        public ActionResult EmployeeList_Partial(string search)
        {
            try
            {
                var EmployeeDetails = _iemployee.Get(search).Result;
                return PartialView("_EmployeeList", EmployeeDetails);
            }
            catch (Exception ex)
            {
                return Json("EmployeeList_Partial" + ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult CreateEmployee()
        {
            try
            {
                tblEmployees oTblEmployees = new tblEmployees();
                return View(oTblEmployees);
            }
            catch (Exception ex)
            {
                return Json("CreateEmployee" + ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult PostEmployee(tblEmployees _oTblEmployees, string dateOfJoining)
        {
            try
            {
               
                DateTimeOffset dateOfJoiningDto;
                if (DateTimeOffset.TryParse(dateOfJoining, out dateOfJoiningDto))
                {
                    _oTblEmployees.DateOfJoining = (float)dateOfJoiningDto.ToUnixTimeSeconds();
                }
                else
                {
                    ModelState.AddModelError("DateOfJoining", "Invalid Date of Joining format.");
                    return View(_oTblEmployees);
                }
                var result = (_oTblEmployees.ID >0) ? _iemployee.Put(_oTblEmployees).Result : _iemployee.Post(_oTblEmployees).Result;
                return RedirectToAction("EmployeeList");
            }
            catch (Exception ex)
            {
                return Json("CreateEmployee" + ex.Message);
            }
        }
        [HttpGet]
        public ActionResult EditEmployee(int Id)
        {
            try
            {
                var Employee = _iemployee.GetEmployeeById(Id).Result;
           
                return View("CreateEmployee", Employee);
            }
            catch (Exception ex)
            {
                return Json("EditEmployee" + ex.Message);
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _iemployee.Delete(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("Delete " + ex.Message);
            }
        }
    }
}
