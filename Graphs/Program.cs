using System;

namespace Graphs{
    class Program{
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            const int length = 10;

            //var random = new Random();
            //var matrix = new float[length, length];
            //for (var x = 0; x < length; x++) {
            //    for (var y = x + 1; y < length; y++) {
            //        matrix[x, y] = (float) (random.NextDouble() < 0.8
            //            ? random.NextDouble() * 100
            //            : double.PositiveInfinity);
            //    }
            //}

            var graph = new Graph(@"D:\graph.dat");
            Console.WriteLine(graph);
            
            var savedPredecessors = new Graph(@"D:\predecessors.dat");
            Console.WriteLine("---------------------------");
            Console.WriteLine(savedPredecessors);
            
            var savedCosts = new Graph(@"D:\cost.dat");
            Console.WriteLine("---------------------------");
            Console.WriteLine(savedCosts);
            
            
            
            
            Console.WriteLine("---------------------------");
            Console.WriteLine("---------------------------");
            graph.GetMinimumCostGraph(out var cost, out var predecessors);
            Console.WriteLine(predecessors);
            Console.WriteLine("---------------------------");
            Console.WriteLine(cost);
            
            graph.Write(@"D:\graph.dat");
        }
    }
}