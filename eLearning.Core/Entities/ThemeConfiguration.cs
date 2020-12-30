using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLearning.Core.Entities
{
    public class ThemeConfiguration
    {
        public Guid Id { get; set; }

        public bool IsLectureEnabled { get; set; }

        public bool IsLabEnabled { get; set; }

        public bool IsQuizEnabled { get; set; }

        public string ExternalQuizId { get; set; }

        public IFormFile LectureFile { get; set; }
        public IFormFile LabFile { get; set; }
    }
}
