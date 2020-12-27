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
    public class MainGraphController : ControllerBase
    {
        private readonly ILogger<MainGraphController> _logger;
        private readonly GraphManager graphManager;

        public MainGraphController(ILogger<MainGraphController> logger, GraphManager graphManager)
        {
            _logger = logger;
            this.graphManager = graphManager;
        }

        [HttpGet]
        public Graph Get()
        
        {
            var graph = graphManager.GetMainGraph();
            return graph;
        }

        [HttpGet]
        [Route("secret")]
        public IEnumerable<string> GetSecret()
        {
            return new List<string>() { "one", "two", "three" };
        }
    }
}
