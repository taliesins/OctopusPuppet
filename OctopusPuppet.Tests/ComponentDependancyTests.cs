﻿using System.Collections.Generic;
using NUnit.Framework;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Graphviz;

namespace OctopusPuppet.Tests
{
    public class ComponentDependancyTests
    {
        [Test(Description = "Sort Components")]
        public void SortComponents()
        {
            var componentDependancies = new AdjacencyGraph<ComponentVertex, ComponentEdge>(true);

            //Add vertices

            var a = new ComponentVertex("a", ComponentAction.Skip);
            var b = new ComponentVertex("b", ComponentAction.Change);
            var c = new ComponentVertex("c", ComponentAction.Skip);
            var d = new ComponentVertex("d", ComponentAction.Change);
            var e = new ComponentVertex("e", ComponentAction.Skip);
            var f = new ComponentVertex("f", ComponentAction.Change);
            var g = new ComponentVertex("g", ComponentAction.Remove);
            var h = new ComponentVertex("h", ComponentAction.Change);

            var x = new ComponentVertex("x", ComponentAction.Skip);
            var y = new ComponentVertex("y", ComponentAction.Change);
            var z = new ComponentVertex("z", ComponentAction.Skip);

            componentDependancies.AddVertexRange(new ComponentVertex[]
            {
                z, b, c, d, e, f, g, h, a, x, y
            });

            //Create edges

            var b_a = new ComponentEdge(b, a);
            var c_b = new ComponentEdge(c, b);
            var b_c = new ComponentEdge(b, c);
            var d_a = new ComponentEdge(d, a);
            var e_d = new ComponentEdge(e, d);
            var f_e = new ComponentEdge(f, e);
            var h_e = new ComponentEdge(h, e);
            var h_d = new ComponentEdge(h, d);
            var g_d = new ComponentEdge(g, d);

            var y_x = new ComponentEdge(y, x);
            var z_y = new ComponentEdge(z, y);

            componentDependancies.AddEdgeRange(new ComponentEdge[]
            {
                z_y, c_b, b_c, d_a, e_d, f_e, h_e, h_d, g_d, b_a, y_x
            });

            var connectedComponents = (IDictionary<ComponentVertex, int>)new Dictionary<ComponentVertex, int>();
            componentDependancies.StronglyConnectedComponents(out connectedComponents);

            foreach (var connectedComponent in connectedComponents)
            {
                connectedComponent.Key.Group = connectedComponent.Value;
            }

            var compressedRowGraph = componentDependancies.ToGraphviz();
        }
    }
}
