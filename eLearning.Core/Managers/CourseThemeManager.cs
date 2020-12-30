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
        private readonly FileStorageManager fileStorageManager;

        public CourseThemeManager(CourseThemeProvider courseThemeProvider, FileStorageManager fileStorageManager)
        {
            this.courseThemeProvider = courseThemeProvider;
            this.fileStorageManager = fileStorageManager;
        }

        public CourseTheme Get(Guid themeId)
        {
            if (themeId == null || themeId == default(Guid))
                throw new ArgumentNullException(nameof(themeId), "Parameter cannot be null.");

            return courseThemeProvider.Get(themeId);
        }

        public CourseTheme Configure(ThemeConfiguration themeConfiguration)
        {
            var theme = courseThemeProvider.Get(themeConfiguration.Id);
            if (theme == null)
                return null;

            if (themeConfiguration.LectureFile != null)
            {
                var path = fileStorageManager.SaveFile(themeConfiguration.LectureFile);
                theme.Lecture.FilePath = path;
            }

            if (themeConfiguration.LabFile != null)
            {
                var path = fileStorageManager.SaveFile(themeConfiguration.LabFile);
                theme.Lab.FilePath = path;
            }

            theme.IsLectureEnabled = themeConfiguration.IsLectureEnabled;
            theme.IsLabEnabled = themeConfiguration.IsLabEnabled;
            theme.IsQuizEnabled = themeConfiguration.IsQuizEnabled;
            theme.Quiz.ExternalQuizId = themeConfiguration.ExternalQuizId;

            return courseThemeProvider.Save(theme);
        }
    }
}
