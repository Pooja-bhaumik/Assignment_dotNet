using Contracts;
using Entites;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class UserLoginController : Controller
    {
        IUserLoginContract _iuserlogin;

        public UserLoginController(IUserLoginContract iuserlogincontract)
        {
            _iuserlogin = iuserlogincontract;
        }

        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult PostUser([FromForm] UserLoginModel _oUserLoginModel)
        {
            try
            {
                tblUsers oTblUsers = new tblUsers();
                oTblUsers.UserName = _oUserLoginModel.UserName;
                oTblUsers.Password = _oUserLoginModel.Password;
                var UserDetails = _iuserlogin.Post(oTblUsers).Result;
                if (UserDetails != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserDetails.UserName),
                    new Claim(ClaimTypes.Role,  UserDetails.UserRole)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                     HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    if (UserDetails.UserRole == "Admin")
                    {
                        return RedirectToAction("EmployeeList", "Employee");
                    }              
                }

                ViewBag.Error = "Invalid username or password";
                return View("Login");
            }
            catch (Exception ex)
            {
                return Json("Get" + ex.Message);
            }
        }
    }
}
