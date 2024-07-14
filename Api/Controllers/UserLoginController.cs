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
    public class UserLoginController : ControllerBase
    {

        UserDbContext _context;


        private readonly ILogger<UserLoginController> _logger;

        public UserLoginController(ILogger<UserLoginController> logger, UserDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var UserDetails = _context.tblUsersRoles.ToList();
                return Ok(UserDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] tblUsers _oTblUsers)
        {
            try
            {

                var userDetails = _context.ViewUsersAndRoleDetails.FirstOrDefault(x => x.UserName == _oTblUsers.UserName);
                if (userDetails != null)
                {
                    string decryptedPassword = DecryptPassword(userDetails.Password);
                    if (decryptedPassword == _oTblUsers.Password)
                    {
                        return Ok(userDetails);
                    }
                }
                return Ok("");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public static string DecryptPassword(string Password)
        {

            byte[] EncryptedPassword = Convert.FromBase64String(Password);
            string DecryptedPassword = ASCIIEncoding.ASCII.GetString(EncryptedPassword);
            return DecryptedPassword;
        }

    }
}
