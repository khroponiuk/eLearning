using eLearning.Core.Data;
using eLearning.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eLearning.Core.Providers
{
    public class GraphProvider
    {
        private readonly ApplicationDbContext dbContext;

        public GraphProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Graph Get(Guid graphId)
        {
            return dbContext.Graphs.FirstOrDefault(x => x.Id == graphId);
        }

        public Graph GetMainGraph()
        {
            return dbContext.Graphs
                .Include(x => x.Nodes)
                .Include(x => x.Edges)
                .FirstOrDefault(x => x.Type == GraphType.MainGraph);
        }

        public Graph GetGraphNode(Guid graphNodeId)
        {
            return null;
        }

        public Graph GetGraphNodeConfigruation(Guid graphNodeId)
        {
            return null;
        }
    }
}
