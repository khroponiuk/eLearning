﻿using eLearning.Core.Data;
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

        protected override void OnAddGraphNode(GraphNode graphNode)
        {
            base.OnAddGraphNode(graphNode);

            var theme = new CourseTheme() { Id = graphNode.Id };
            this.dbContext.CourseThemes.Add(theme);

            var lecture = new Lecture() { Id = Guid.NewGuid(), CourseThemeId = theme.Id };
            this.dbContext.Lectures.Add(lecture);

            var lab = new Lab() { Id = Guid.NewGuid(), CourseThemeId = theme.Id };
            this.dbContext.Labs.Add(lab);

            var quiz = new Quiz() { Id = Guid.NewGuid(), CourseThemeId = theme.Id };
            this.dbContext.Quizzes.Add(quiz);
        }

        protected override void OnRemoveGraphNode(GraphNode graphNode)
        {
            //var graphEdges = this.dbContext.GraphEdges.Where(x => x.GraphId == graphNode.Id);
            //if (graphEdges.Any())
            //    this.dbContext.GraphEdges.RemoveRange(graphEdges);

            //var graphNodes = this.dbContext.GraphNodes.Where(x => x.GraphId == graphNode.Id);
            //if (graphNodes.Any())
            //    this.dbContext.GraphNodes.RemoveRange(graphNodes);

            //var courseGraph = this.dbContext.Graphs.FirstOrDefault(x => x.Id == graphNode.Id);
            //if (courseGraph != null)
            //    this.dbContext.Graphs.Remove(courseGraph);
        }
    }
}
