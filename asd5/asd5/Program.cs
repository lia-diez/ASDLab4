using System;

namespace asd5
{
    public static class Program
    {
        public static void Main()
        {
            Player player = new Player(new Board(4, 4, 1), 5);
            player.GenerateDecisionTree();
            Console.WriteLine(player.NextStep);
        }
    }
}