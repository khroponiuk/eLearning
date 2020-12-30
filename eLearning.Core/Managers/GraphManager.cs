using eLearning.Core.Entities;
using eLearning.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLearning.Core.Managers
{
    public class GraphManager
    {
        private readonly GraphProvider graphProvider;

        public GraphManager(GraphProvider graphProvider)
        {
            this.graphProvider = graphProvider;
        }

        public Graph Get(Guid graphId)
        {
            if (graphId == null)
                throw new ArgumentNullException(nameof(graphId), "Parameter cannot be null.");

            return graphProvider.Get(graphId);
        }

        public Graph GetMainGraph()
        {
            var graph = graphProvider.GetMainGraph();

            return graph;
        }

        public Graph Save(Graph graph)
        {
            return graphProvider.Save(graph);
        }
    }
}
