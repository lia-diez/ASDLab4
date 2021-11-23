using System;
using System.Collections.Generic;

namespace ASDlab4
{
    public class AntGraph : Graph
    {
        public float[,] Pheromones;
        public float Alpha;
        public float Beta;
        public float Evaporation;
        public float AntPheromone;
        public AntGraph(int vertexCount, float alpha, float beta, float evaporation, float antPheromone) : base(vertexCount)
        {
            Alpha = alpha;
            Beta = beta;
            Evaporation = evaporation;
            AntPheromone = antPheromone;
            Pheromones = new float[VertexCount, VertexCount];
        }

        public int AntSearch(int count, int antCount)
        {
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                result = int.MaxValue;
                for (int j = 0; j < antCount; j++)
                {
                    Ant ant = new Ant(this, 0);
                    ant.FindRoute();
                    var length = ant.GetLength();
                    Console.WriteLine(length);
                    if (length < result)
                    {
                        result = ant.GetLength();
                    }
                }
            }
            
            return result;
        }
    }
}