using eLearning.Core.Data;
using eLearning.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLearning.Core.Providers.GraphUpdateStrategies
{
    public class CourseGraphUpdateStrategy : GraphUpdateStrategy
    {
        public CourseGraphUpdateStrategy(ApplicationDbContext dbContext, Graph incomingGraph) : base(dbContext, incomingGraph)
        {
        }
    }
}
