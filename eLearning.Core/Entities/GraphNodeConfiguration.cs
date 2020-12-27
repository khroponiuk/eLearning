using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eLearning.Core.Entities
{
    public class GraphNodeConfiguration
    {
        [Key]
        public Guid GraphNodeId { get; set; }

        public bool LectureEnabled { get; set; }

        public bool LabEnabled { get; set; }

        public bool TestsEnabled { get; set; }

        public GraphNode GraphNode { get; set; }
    }
}
