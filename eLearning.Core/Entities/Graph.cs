using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace eLearning.Core.Entities
{
    public class Graph
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Scale { get; set; }

        public double TranslateX { get; set; }

        public double TranslateY { get; set; }


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
