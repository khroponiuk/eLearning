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

        public Graph Create(Graph graphId)
        {
            if (graphId == null)
                throw new ArgumentNullException(nameof(graphId), "Parameter cannot be null.");

            return graphProvider.Get(Guid.Empty);
        }

        void ReevaluateGraphNodeStatus(Guid graphNodeId)
        {
            //var nodeConfiguration = GetGraphNodeConfiguration(graphNodeId)
            //if (nodeConfiguration.labEnabled)
            //  var lab = GetLabSubmission(graphNodeId)
            //  if (lab == null)
            //    return;
            // ...
            // SetGraphNodeCompletion(graphNodeId)
        }

        public Graph Save(Graph graph)
        {
            return graphProvider.Save(graph);
        }
    }
}
