using eLearning.Core.Data;
using eLearning.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eLearning.Core.Providers.GraphUpdateStrategies
{
    public class GraphUpdateStrategy
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly Graph incomingGraph;
        protected readonly Graph currentGraph;

        public GraphUpdateStrategy(ApplicationDbContext dbContext, Graph incomingGraph)
        {
            this.dbContext = dbContext;
            this.incomingGraph = incomingGraph;
            this.currentGraph = GetCurrentGraph();
        }

        public Graph Execute()
        {
            UpdateCurrentGraph();
            DetectRemovedGraphNodes();
            DetectRemovedGraphEdges();
            DetectAddedOrUpdatedGraphNodes();
            DetectAddedOrUpdatedGraphEdges();

            dbContext.SaveChanges();

            return currentGraph;
        }


        Graph GetCurrentGraph()
        {
            return dbContext.Graphs
                .Include(x => x.Nodes)
                .Include(x => x.Edges)
                .FirstOrDefault(x => x.Id == incomingGraph.Id);
        }

        void UpdateCurrentGraph()
        {
            currentGraph.Name = incomingGraph.Name;
            currentGraph.Scale = incomingGraph.Scale;
            currentGraph.TranslateX = incomingGraph.TranslateX;
            currentGraph.TranslateY = incomingGraph.TranslateY;
        }

        void DetectRemovedGraphNodes()
        {
            foreach (var existingNode in currentGraph.Nodes)
            {
                if (!incomingGraph.Nodes.Any(c => c.Id == existingNode.Id))
                {
                    dbContext.GraphNodes.Remove(existingNode);
                    OnRemoveGraphNode(existingNode);
                }
            }
        }

        void DetectRemovedGraphEdges()
        {
            foreach (var existingEdge in currentGraph.Edges)
            {
                if (!incomingGraph.Edges.Any(c => c.Id == existingEdge.Id))
                {
                    dbContext.GraphEdges.Remove(existingEdge);
                    OnRemoveGraphEdge(existingEdge);
                }
            }
        }

        void DetectAddedOrUpdatedGraphNodes()
        {
            foreach (var incomingNode in incomingGraph.Nodes)
            {
                var existingNode = currentGraph.Nodes
                    .FirstOrDefault(x => x.Id == incomingNode.Id);

                if (existingNode != null)
                    dbContext.Entry(existingNode).CurrentValues.SetValues(incomingNode);
                else
                {
                    var node = new GraphNode()
                    {
                        Id = incomingNode.Id,
                        Name = incomingNode.Name,
                        X = incomingNode.X,
                        Y = incomingNode.Y,
                        GraphId = incomingNode.GraphId
                    };

                    dbContext.GraphNodes.Add(node);
                    OnAddGraphNode(node);
                }
            }
        }

        void DetectAddedOrUpdatedGraphEdges()
        {
            foreach (var incomingEdge in incomingGraph.Edges)
            {
                var existingEdge = currentGraph.Edges
                    .FirstOrDefault(x => x.Id == incomingEdge.Id);

                if (existingEdge != null)
                    dbContext.Entry(existingEdge).CurrentValues.SetValues(incomingEdge);
                else
                {
                    var edge = new GraphEdge()
                    {
                        Id = incomingEdge.Id,
                        SourceNodeId = incomingEdge.SourceNodeId,
                        TargetNodeId = incomingEdge.TargetNodeId,
                        GraphId = incomingEdge.GraphId
                    };

                    dbContext.GraphEdges.Add(edge);
                    OnAddGraphEdge(edge);
                }
            }
        }

        protected virtual void OnRemoveGraphNode(GraphNode graphNode) { }
        protected virtual void OnAddGraphNode(GraphNode graphNode) { }
        protected virtual void OnRemoveGraphEdge(GraphEdge graphEdge) { }
        protected virtual void OnAddGraphEdge(GraphEdge graphEdge) { }
    }
}
