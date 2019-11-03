using System;

namespace Graphs{
    class Program{
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            const int length = 10;

            var random = new Random();
            var matrix = new float[length, length];
            for (var x = 0; x < length; x++) {
                for (var y = x + 1; y < length; y++) {
                    matrix[x, y] = (float) (random.NextDouble() < 0.8 ? random.NextDouble() * 100 : double.PositiveInfinity);
                }
            }
            
            var graph = new Graph(matrix);
            Console.WriteLine(graph);
            Console.WriteLine("---------------------------");
            Console.WriteLine(graph.GetMinimumCostGraph());
        }
    }
}