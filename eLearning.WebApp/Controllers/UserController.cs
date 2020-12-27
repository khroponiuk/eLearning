using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLearning.Core.Entities;
using eLearning.Core.Managers;
using eLearning.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eLearning.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ILogger<UserController> logger, UserManager<ApplicationUser> userManager)
        {
            this._logger = logger;
            this.userManager = userManager;
        }

        [Route("IsInRole")]
        public bool IsInRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return false;

            return User.IsInRole(roleName);
        }
    }
}
