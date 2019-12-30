using System;
using System.IO;
using System.Text;

namespace Graphs{
    /**
     * Represents a column-stored graph.
     */
    public class Graph{
        private readonly float[,] _matrix;
        private readonly int _length;

        public Graph(float[,] matrix) {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Matrix must be a square matrix.");
            _matrix = matrix;
            _length = matrix.GetLength(0);

            //Let's make sure diagonals are 0
            for (var i = 0; i < _length; i++) {
                _matrix[i, i] = 0;
            }

            //Let's make sure it's not directed
            for (var x = 0; x < _length; x++) {
                for (var y = x + 1; y < _length; y++) {
                    _matrix[y, x] = _matrix[x, y];
                }
            }
        }

        public Graph(int length) {
            if (length <= 0)
                throw new ArgumentException("Length must be greater than 0.");
            _length = length;
            _matrix = new float[length, length];
        }

        public Graph(Graph graph) {
            _matrix = graph._matrix.Clone() as float[,];
            if (_matrix == null) {
                throw new Exception("Matrix clone failed.");
            }

            _length = _matrix.GetLength(0);
        }

        public Graph(string path) {
            var stream = new BinaryReader(File.OpenRead(path), Encoding.ASCII);
            _length = stream.ReadInt32();
            if (_length <= 0) {
                stream.Close();
                throw new ArgumentException("Length must be greater than 0.");
            }

            _matrix = new float[_length, _length];

            for (var row = 0; row < _length; row++) {
                for (var column = 0; column < _length; column++) {
                    _matrix[row, column] = stream.ReadSingle();
                }
            }

            stream.Close();
        }

        public float[,] Matrix => _matrix;

        public float GetValue(int x, int y) {
            return _matrix[x, y];
        }

        public void SetValue(int x, int y, float value) {
            _matrix[x, y] = value;
        }

        public void GetMinimumCostGraph(out Graph costGraph, out Graph predecessorsGraph) {
            predecessorsGraph = new Graph(_length);
            for (var i = 0; i < _length; i++) {
                for (var j = 0; j < _length; j++) {
                    predecessorsGraph[i, j] = i == j || float.IsPositiveInfinity(_matrix[i, j]) ? float.NaN : i;
                }
            }

            costGraph = new Graph(this);
            for (var k = 0; k < _length; k++) {
                for (var i = 0; i < _length; i++) {
                    for (var j = 0; j < _length; j++) {
                        var dt = costGraph[i, k] + costGraph[k, j];
                        if (costGraph[i, j] <= dt) continue;
                        costGraph[i, j] = dt;
                        predecessorsGraph[i, k] = predecessorsGraph[k, j];
                    }
                }
            }
        }

        public override string ToString() {
            var builder = new StringBuilder();
            for (var y = 0; y < _length; y++) {
                for (var x = 0; x < _length; x++) {
                    builder.Append(Math.Round(_matrix[x, y], 2));
                    if (x == _length - 1) continue;
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

        public void Write(string path) {
            var stream = new BinaryWriter(File.OpenWrite(path), Encoding.ASCII);
            stream.Write(_length);

            for (var row = 0; row < _length; row++) {
                for (var column = 0; column < _length; column++) {
                    stream.Write(_matrix[row, column]);
                }
            }

            stream.Close();
        }
    }
}