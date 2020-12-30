using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace eLearning.Core.Entities
{
    public class QuizResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuizAttemptId { get; set; }

        public int ExternalQuizId { get; set; }

        public Guid UserId { get; set; }

        public int NumberOfCorrectAnswers { get; set; }

        public int NumberOfWrongAnswers { get; set; }

        public int NumberOfUnanswered { get; set; }

        public double TotalScore { get; set; }
    }
}
