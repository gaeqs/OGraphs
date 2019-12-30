using System;

namespace Graphs{
    internal static class Program{
        private static void Main(string[] args) {
            if (args.Length == 0) return;
            switch (args[0].ToLower()) {
                case "--check":
                    Check();
                    break;
                case "--example":
                    GenerateExampleGraph();
                    break;
                case "--full":
                    Generate50Graph();
                    break;
            }
        }


        private static void GenerateExampleGraph() {
            const float i = float.PositiveInfinity;
            var data = new[] {
                0, 171, 365, i, i, i, i,
                171, 0, 455, i, i, i, i,
                365, 455, 0, 280, 193, i, i,
                i, i, 280, 0, 395, i, 324,
                i, i, 193, 395, 0, 335, 325,
                i, i, i, i, 335, 0, i,
                i, i, i, 324, 325, i, 0
            };
            var matrix = new float[7, 7];
            for (var x = 0; x < 7; x++) {
                for (var y = 0; y < 7; y++) {
                    matrix[x, y] = data[x * 7 + y];
                }
            }

            var graph = new Graph(matrix);
            graph.Write(@"graphExample.dat");
            Console.WriteLine("Example graph generated.");
        }

        private static void Generate50Graph() {
            const int length = 50;

            var random = new Random();
            var matrix = new float[length, length];
            for (var x = 0; x < length; x++) {
                for (var y = x + 1; y < length; y++) {
                    matrix[x, y] = (float) (random.NextDouble() < 0.6
                        ? random.NextDouble() * 100
                        : double.PositiveInfinity);
                }
            }

            var graph = new Graph(matrix);
            graph.Write(@"graph50.dat");
            Console.WriteLine("50 length graph generated.");
        }

        static void Check() {
            var graph = new Graph(@"graph.dat");
            var savedPredecessors = new Graph(@"predecessors.dat");
            var savedCosts = new Graph(@"cost.dat");

            graph.GetMinimumCostGraph(out var cost, out var predecessors);

            Console.WriteLine("Graph: ");
            Console.WriteLine(graph);

            Console.WriteLine("---------------------------");
            Console.WriteLine("GRAPHS CREATED USING ASSEMBLY");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Predecessors:");
            Console.WriteLine(savedPredecessors);

            Console.WriteLine("---------------------------");
            Console.WriteLine("Costs:");
            Console.WriteLine(savedCosts);

            Console.WriteLine("---------------------------");
            Console.WriteLine("GRAPHS CREATED USING C#");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Predecessors:");
            Console.WriteLine(predecessors);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Costs:");
            Console.WriteLine(cost);
        }
    }
}