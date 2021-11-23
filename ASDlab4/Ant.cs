using System;
using System.Collections.Generic;
using System.Linq;

namespace ASDlab4
{
    public class Ant
    {
        private int _position;
        public int Position
        {
            get => _position;
            set
            {
                _position = value;
                Visited.Add(value);
            }
        }
        public List<int> Visited;
        public AntGraph Parent;

        public Ant(AntGraph parent, int position)
        {
            Parent = parent;
            Visited = new List<int> {};
            Position = position;
        }

        public List<int> FindRoute()
        {
            while (Visited.Count < Parent.VertexCount)
            {
                MoveForward();
            }
            SpreadPheromones();
            return Visited;
        }

        private void MoveForward()
        {
            var probability = new float[Parent.VertexCount];
            for (int i = 0; i < Parent.VertexCount; i++)
            {
                if (!Visited.Contains(i))
                {
                    probability[i] = MathF.Pow(Parent.Pheromones[Position, i], Parent.Alpha) + MathF.Pow(1f/Parent.Edges[Position, i], Parent.Beta);
                }
            }
            var sum = probability.Sum();
            for (int i = 0; i < probability.Length; i++)
            {
                probability[i] /= sum;
            }
            Position = SelectRandom(probability);
        }

        private int SelectRandom(float[] probability)
        {
            Random random = new Random();
            float choice = (float) random.NextDouble();
            for (int i = 0; i < probability.Length; i++)
            {
                if (choice < probability[i])
                {
                    return i;
                }
                choice -= probability[i];
            }
            return 0;
        }

        private void SpreadPheromones()
        {
            float deltaPheromone = Parent.AntPheromone/GetLength();
            for (int i = 0; i < Parent.VertexCount; i++)
            {
                for (int j = 0; j < Parent.VertexCount; j++)
                {
                    Parent.Pheromones[i, j] *= 1 - Parent.Evaporation;
                }
            }
            for (int i = 0; i < Parent.VertexCount-1; i++)
            {
                Parent.Pheromones[Visited[i], Visited[i + 1]] += deltaPheromone;
            }
        }

        public int GetLength()
        {
            int length = 0;
            for (int i = 0; i < Parent.VertexCount-1; i++)
            {
                length += Parent.Edges[Visited[i], Visited[i + 1]];
            }
            return length;
        }
    }
}