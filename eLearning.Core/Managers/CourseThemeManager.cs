using eLearning.Core.Entities;
using eLearning.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLearning.Core.Managers
{
    public class CourseThemeManager
    {
        private readonly CourseThemeProvider courseThemeProvider;

        public CourseThemeManager(CourseThemeProvider courseThemeProvider)
        {
            this.courseThemeProvider = courseThemeProvider;
        }

        public CourseTheme Get(Guid themeId)
        {
            if (themeId == null || themeId == default(Guid))
                throw new ArgumentNullException(nameof(themeId), "Parameter cannot be null.");

            return courseThemeProvider.Get(themeId);
        }

        public CourseTheme Save(CourseTheme courseTheme)
        {
            return courseThemeProvider.Save(courseTheme);
        }
    }
}
