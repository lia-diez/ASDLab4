
using System;
using System.IO;

namespace ASDlab4
{
    class Program
    {
        static void Main(string[] args)
        {
            AntGraph ant = new AntGraph(20, 1f, 1f, 0.3f, 100);
            ant.GenerateRandom(5, 150);
            Console.WriteLine(ant);
            ant.AntSearch(100, 10);
            //ant.AntSearch(1000, 10);
            /*int min = int.MaxValue;
            float mina = 0;
            float minb = 1;
            for (float a = 0; a <= 5; a+=0.3f)
            {
                for (float b = 1; b <=5 ; b+=0.3f)
                {
                    ant.Alpha = a;
                    ant.Beta = b;
                    ant.Pheromones.Initialize();
                    var res = ant.AntSearch(100, 10);
                    if (res < min)
                    {
                        mina = a;
                        minb = b;
                    }
                    Console.WriteLine($"{a:F1} {b:F1} {res}");
                }
            }

            Console.WriteLine("=========================");
            Console.WriteLine($"A:{mina}, B{minb}: {min}");*/
            /*var maxAlpha = 0f;
            var maxAlphaValue = int.MaxValue;
            for (float i = 0; i < 10; i+=0.2f)
            {
                ant.Alpha= i;
                int current = ant.AntSearch(100, 10);
                if (current < maxAlphaValue)
                {
                    maxAlphaValue = current;
                    maxAlpha = i;
                }

                Console.WriteLine(current);
            }

            Console.WriteLine("==============");
            Console.WriteLine(maxAlpha);*/
        }
    }
}