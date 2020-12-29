using eLearning.Core.Data;
using eLearning.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eLearning.Core.Providers.GraphUpdateStrategies
{
    public class MainGraphUpdateStrategy : GraphUpdateStrategy
    {
        public MainGraphUpdateStrategy(ApplicationDbContext dbContext, Graph incomingGraph)
            : base(dbContext, incomingGraph)
        {
        }

        protected override void OnAddGraphNode(GraphNode graphNode)
        {
            base.OnAddGraphNode(graphNode);

            var courseGraph = new Graph() { Id = graphNode.Id, Name = graphNode.Name, Type = GraphType.CourseGraph, Scale = 1.0 };
            this.dbContext.Graphs.Add(courseGraph);
        }

        protected override void OnRemoveGraphNode(GraphNode graphNode)
        {
            var graphEdges = this.dbContext.GraphEdges.Where(x => x.GraphId == graphNode.Id);
            if (graphEdges.Any())
                this.dbContext.GraphEdges.RemoveRange(graphEdges);

            var graphNodes = this.dbContext.GraphNodes.Where(x => x.GraphId == graphNode.Id);
            if (graphNodes.Any())
                this.dbContext.GraphNodes.RemoveRange(graphNodes);

            var courseGraph = this.dbContext.Graphs.FirstOrDefault(x => x.Id == graphNode.Id);
            if (courseGraph != null)
                this.dbContext.Graphs.Remove(courseGraph);
        }
    }
}
