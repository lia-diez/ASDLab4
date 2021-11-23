using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ASDlab4
{
    public class Graph
    {
        public int VertexCount { get; }
        public int[,] Edges { get; }

        public Graph(int vertexCount)
        {
            VertexCount = vertexCount;
            Edges = new int[vertexCount, vertexCount];
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < VertexCount; i++)
            {
                for (int j = 0; j < VertexCount; j++)
                {
                    builder.Append($"{Edges[i,j]} ");
                }
                builder.Append('\n');
            }
            return builder.ToString();
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            if (vertex1 >= VertexCount || vertex2 >= VertexCount)
                throw new ArgumentException();
            Edges[vertex1, vertex2] = weight;
            //Edges[vertex2, vertex1] = true;
        }

        public void GenerateRandom(int min, int max)
        {
            Random random = new Random();
            for (int i = 0; i < VertexCount; i++)
            {
                for (int j = 0; j < VertexCount; j++)
                {
                    if(i != j) Edges[i, j] = random.Next(min, max);
                }
            }
        }

        public int Max()
        {
            int max = Edges[0, 0];
            for (int i = 0; i < VertexCount; i++)
            {
                for (int j = 0; j < VertexCount; j++)
                {
                    if (Edges[i, j] > max)
                    {
                        max = Edges[i, j];
                    }
                }
            }

            return max;
        }

        public void Serialize(string path)
        {
            byte type = 0;
            if (VertexCount > 255 || Max() > 255) type = 1;
            List<byte> output = new List<byte>();
            output.Add(type);
            if (type == 1)
            {
                output.AddRange(BitConverter.GetBytes(VertexCount));
                for (int i = 0; i < VertexCount; i++)
                {
                    for (int j = 0; j < VertexCount; j++)
                    {
                        output.AddRange(BitConverter.GetBytes(Edges[i, j]));
                    }
                }
            }
            else
            {
                output.Add((byte) VertexCount);
                for (int i = 0; i < VertexCount; i++)
                {
                    for (int j = 0; j < VertexCount; j++)
                    {
                        output.Add((byte)Edges[i, j]);
                    }
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Truncate)))
            {
                writer.Write(output.ToArray());
            }
        }
    }
}