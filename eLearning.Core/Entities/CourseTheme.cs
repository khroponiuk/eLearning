﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eLearning.Core.Entities
{
    public class CourseTheme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsLectureEnabled { get; set; } = true;
        public bool IsLabEnabled { get; set; } = true;
        public bool IsQuizEnabled { get; set; } = true;

        public Lecture Lecture { get; set; }

        public Lab Lab { get; set; }

        public Quiz Quiz { get; set; }
    }

    public class Lecture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FilePath { get; set; }

        public Guid CourseThemeId { get; set; }

        public CourseTheme CourseTheme { get; set; }
    }

    public class Lab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FilePath { get; set; }

        public Guid CourseThemeId { get; set; }

        public CourseTheme CourseTheme { get; set; }
    }

    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ExternalQuizId { get; set; }

        public string QuizSnippet { get; set; }

        public Guid CourseThemeId { get; set; }

        public CourseTheme CourseTheme { get; set; }
    }
}
