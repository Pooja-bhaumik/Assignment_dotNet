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
    public class UserRegistrationController : ControllerBase
    {

        UserDbContext _context;


        private readonly ILogger<UserRegistrationController> _logger;

        public UserRegistrationController(ILogger<UserRegistrationController> logger, UserDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] tblUsers _oTblUsers)
        {
            try
            {
                if (_oTblUsers != null)
                {
                    string EncPassword = EncryptPassword(_oTblUsers.Password);
                    _oTblUsers.Password = EncPassword;
                    _context.tblUsers.Add(_oTblUsers);
                    _context.SaveChanges();
                    return Ok(new { message = "User registered successfully" });
                }
                else
                {
                    return Ok("Registration Failed");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        public static string EncryptPassword(string Password)
        {
            byte[] StorePassword = Encoding.ASCII.GetBytes(Password);
            string EncryptedPassword = Convert.ToBase64String(StorePassword);
            return EncryptedPassword;
        }
    }
}
