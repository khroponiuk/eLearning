using eLearning.Core.Data;
using eLearning.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eLearning.Core.Providers
{
    public class CourseThemeProvider
    {
        private readonly ApplicationDbContext dbContext;

        public CourseThemeProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CourseTheme Get(Guid themeId)
        {
            return dbContext.CourseThemes
                .Include(x => x.Lecture)
                .Include(x => x.Lab)
                .Include(x => x.Quiz)
                .FirstOrDefault(x => x.Id == themeId);
        }

        public CourseTheme Save(CourseTheme courseTheme)
        {
           var currentEntity = dbContext.CourseThemes
                .Include(x => x.Lecture)
                .Include(x => x.Lab)
                .Include(x => x.Quiz)
                .FirstOrDefault(x => x.Id == courseTheme.Id);

            if (currentEntity == null)
                return null;

            currentEntity.IsLectureEnabled = courseTheme.IsLectureEnabled;
            currentEntity.IsLabEnabled = courseTheme.IsLabEnabled;
            currentEntity.IsQuizEnabled = courseTheme.IsQuizEnabled;

            if (currentEntity.Lecture == null && courseTheme.Lecture != null)
            {
                dbContext.Lectures.Add(courseTheme.Lecture);
            }

            if (currentEntity.Lab == null && courseTheme.Lab != null)
            {
                dbContext.Labs.Add(courseTheme.Lab);
            }

            if (currentEntity.Quiz == null && courseTheme.Quiz != null)
            {
                dbContext.Quizzes.Add(courseTheme.Quiz);
            }

            currentEntity.Quiz = courseTheme.Quiz;
            currentEntity.Lecture = courseTheme.Lecture;
            currentEntity.Lab = courseTheme.Lab;

            return currentEntity;

        }
    }
}
