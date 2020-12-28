using eLearning.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLearning.Core.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedGraphData(modelBuilder);
            //SeedUserData(modelBuilder);
        }

        private static void SeedGraphData(ModelBuilder modelBuilder)
        {
            var graph = new Graph()
            {
                Id = Guid.NewGuid(),
                Name = "Main graph",
                Type = GraphType.MainGraph,
                Scale = 1.0,
                TranslateX = 0,
                TranslateY = 0
            };

            modelBuilder.Entity<Graph>().HasData(graph);

            var nodes = new List<GraphNode>()
            {
                new GraphNode() { Id = Guid.NewGuid(), X = 550, Y = 270, Name = "Intro", GraphId = graph.Id },
                new GraphNode() { Id = Guid.NewGuid(), X = 750, Y = 370, Name = "Topic", GraphId = graph.Id }
            };


            modelBuilder.Entity<GraphNode>().HasData(nodes);

            modelBuilder.Entity<GraphEdge>().HasData(
                new GraphEdge() { Id = Guid.NewGuid(), SourceNodeId = nodes[0].Id, TargetNodeId = nodes[1].Id, GraphId = graph.Id }
            );
        }

        private static void SeedUserData(ModelBuilder modelBuilder)
        {
           
        }
    }
}
