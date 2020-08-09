using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orchestrate.Identity.API.Models;

namespace Orchestrate.Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        public AppUsersController(UserManager<IdentityUser> userManager) :base()
        {
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            List<UserInfo> userInfo = new List<UserInfo>();

            foreach (var user in users)
            {
                userInfo.Add(new UserInfo
                {
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                });
            }

            return Ok(userInfo.ToArray());
        }
    }
}