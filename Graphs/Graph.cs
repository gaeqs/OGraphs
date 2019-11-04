using System;
using System.Text;

namespace Graphs{
    /**
     * Represents a column-stored graph.
     */
    public class Graph{
        private readonly float[,] matrix_;
        private readonly int length_;

        public Graph(float[,] matrix) {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Matrix must be a square matrix.");
            matrix_ = matrix;
            length_ = matrix.GetLength(0);

            //Let's make sure diagonals are 0
            for (var i = 0; i < length_; i++) {
                matrix_[i, i] = 0;
            }

            //Let's make sure it's not directed
            for (var x = 0; x < length_; x++) {
                for (var y = x + 1; y < length_; y++) {
                    matrix_[y, x] = matrix_[x, y];
                }
            }
        }

        public Graph(int length) {
            if (length <= 0)
                throw new ArgumentException("Length must be greater than 0.");
            length_ = length;
            matrix_ = new float[length, length];
        }

        public Graph(Graph graph) {
            matrix_ = graph.matrix_.Clone() as float[,];
            if (matrix_ == null) {
                throw new Exception("Matrix clone failed.");
            }

            length_ = matrix_.GetLength(0);
        }

        public float[,] Matrix => matrix_;

        public float GetValue(int x, int y) {
            return matrix_[x, y];
        }

        public void SetValue(int x, int y, float value) {
            matrix_[x, y] = matrix_[y, x] = value;
        }

        public void GetMinimumCostGraph(out Graph costGraph, out Graph predecessorsGraph) {
            costGraph = new Graph(this);

           predecessorsGraph = new Graph(length_);
            for (var i = 0; i < length_; i++) {
                for (var j = 0; j < length_; j++) {
                    predecessorsGraph[i, j] = i == j || float.IsPositiveInfinity(matrix_[i, j]) ? float.NaN : i;
                }
            }
            
            

            for (var k = 0; k < length_; k++) {
                for (var i = 0; i < length_; i++) {
                    for (var j = 0; j < length_; j++) {
                        var dt = costGraph[i, k] + costGraph[k, j];
                        if (costGraph[i, j] < dt) continue;
                        costGraph[i, j] = dt;
                    }
                }
            }
        }

        public override string ToString() {
            var builder = new StringBuilder();
            for (var y = 0; y < length_; y++) {
                for (var x = 0; x < length_; x++) {
                    builder.Append(Math.Round(matrix_[x, y], 2));
                    if (x == length_ - 1) continue;
                    builder.Append("\t");
                }

                builder.Append('\n');
            }

            return builder.ToString();
        }

        public float this[int x, int y] {
            get => GetValue(x, y);
            set => SetValue(x, y, value);
        }
    }
}