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
            //graph.Write(@"D:\graph.dat");

            var graph = new Graph(@"C:\Users\g.rial.2018\OneDrive - Universidad Rey Juan Carlos\Universidad\Organización de Computadores\graph.dat");
            Console.WriteLine(graph);
            
            var savedPredecessors = new Graph(@"C:\Users\g.rial.2018\OneDrive - Universidad Rey Juan Carlos\Universidad\Organización de Computadores\predecessors.dat");
            Console.WriteLine("---------------------------");
            Console.WriteLine(savedPredecessors);
            
            var savedCosts = new Graph(@"C:\Users\g.rial.2018\OneDrive - Universidad Rey Juan Carlos\Universidad\Organización de Computadores\cost.dat");
            Console.WriteLine("---------------------------");
            Console.WriteLine(savedCosts);
            
            
            
            
            Console.WriteLine("---------------------------");
            Console.WriteLine("---------------------------");
            graph.GetMinimumCostGraph(out var cost, out var predecessors);
            Console.WriteLine(predecessors);
            Console.WriteLine("---------------------------");
            Console.WriteLine(cost);
        }
    }
}