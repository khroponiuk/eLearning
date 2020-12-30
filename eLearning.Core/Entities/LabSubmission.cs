using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace eLearning.Core.Entities
{
    public class LabSubmission
    {
        public Guid Id { get; set; }

        public Guid LabId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string RepoLink { get; set; }

        public int Score { get; set; }

        public DateTime SubmissionTime { get; set; }
    }
}
