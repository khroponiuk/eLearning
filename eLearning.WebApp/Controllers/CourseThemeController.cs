using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLearning.Core.Entities;
using eLearning.Core.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eLearning.WebApp.Controllers
{
    [ApiController]
    [Route("api/theme")]
    public class CourseThemeController : ControllerBase
    {
        private readonly ILogger<CourseThemeController> _logger;
        private readonly CourseThemeManager courseThemeManager;

        public CourseThemeController(ILogger<CourseThemeController> logger, CourseThemeManager courseThemeManager)
        {
            _logger = logger;
            this.courseThemeManager = courseThemeManager;
        }

        [HttpGet]
        public CourseTheme Get(Guid id)
        {
            var courseTheme = courseThemeManager.Get(id);
            return courseTheme;
        }

        [HttpPost]
        [Route("Configure")]
        public CourseTheme Configure([FromForm]ThemeConfiguration themeConfiguration)
        {
            return courseThemeManager.Configure(themeConfiguration);
        }
    }
}
