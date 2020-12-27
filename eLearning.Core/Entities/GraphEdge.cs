using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eLearning.Core.Entities
{
    public class GraphEdge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SourceNodeId { get; set; }

        public Guid TargetNodeId { get; set; }

        public Graph Graph { get; set; }

        public Guid GraphId { get; set; }
    }
}
