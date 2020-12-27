using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eLearning.Core.Entities
{
    public class GraphNode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public string Name { get; set; }

        public Graph Graph { get; set; }

        public Guid GraphId { get; set; }

        //public GraphNodeConfiguration Configuration{ get; set; }
    }
}
