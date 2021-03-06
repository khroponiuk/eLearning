﻿using eLearning.Core.Data;
using eLearning.Core.Entities;
using eLearning.Core.Providers.GraphUpdateStrategies;
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
            return dbContext.Graphs
                .Include(x => x.Nodes)
                .Include(x => x.Edges)
                .FirstOrDefault(x => x.Id == graphId);
        }

        public Graph GetMainGraph()
        {
            return dbContext.Graphs
                .Include(x => x.Nodes)
                .Include(x => x.Edges)
                .FirstOrDefault(x => x.Type == GraphType.MainGraph);
        }

        public Graph Save(Graph graph)
        {
            var updateStrategy = GetGraphUdateStrategy(graph);
            
            return updateStrategy.Execute();
        }

        private GraphUpdateStrategy GetGraphUdateStrategy(Graph graph)
        {
            if (graph.Type == GraphType.MainGraph)
                return new MainGraphUpdateStrategy(dbContext, graph);
            
            return new CourseGraphUpdateStrategy(dbContext, graph);
        }
    }
}
