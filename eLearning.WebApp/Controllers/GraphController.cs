using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLearning.Core.Entities;
using eLearning.Core.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eLearning.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;
        private readonly GraphManager graphManager;

        public GraphController(ILogger<GraphController> logger, GraphManager graphManager)
        {
            _logger = logger;
            this.graphManager = graphManager;
        }

        [HttpGet]
        public Graph GetGraph(Guid graphId)
        {
            var graph = graphManager.Get(graphId);
            return graph;
        }

        [HttpGet]
        [Route("Main")]
        public Graph GetMainGraph()
        {
            var graph = graphManager.GetMainGraph();
            return graph;
        }

        [HttpPost]
        [Route("Save")]
        public Graph SaveGraph(Graph graph)
        {
            return graphManager.Save(graph);
        }
    }
}
