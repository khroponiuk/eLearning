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
        [Route("Save")]
        public CourseTheme SaveGraph(CourseTheme courseTheme)
        {
            return courseThemeManager.Save(courseTheme);
        }

        [HttpPost]
        [Route("Configure")]
        public CourseTheme Configure(ThemeConfiguration themeConfiguration)
        {
            return null;
        }
    }

    public class ThemeConfiguration
    {
        public Guid id { get; set; }
        public bool isLectureEnabled { get; set; }
        public bool isLabEnabled { get; set; }
        public bool isQuizEnabled { get; set; }
        public IFormFile lectureFile { get; set; }
        public IFormFile labFile { get; set; }
    }
}
