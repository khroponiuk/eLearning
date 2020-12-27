using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eLearning.Core.Entities
{
    public class Graph
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public GraphType Type { get; set; }

        public IList<GraphNode> Nodes { get; set; }

        public IList<GraphEdge> Edges { get; set; }
    }

    public enum GraphType
    {
        MainGraph,
        CourseGraph
    }
}
