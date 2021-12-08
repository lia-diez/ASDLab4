using System;

namespace asd5
{
    public static class Program
    {
        public static void Main()
        {
            Player player = new Player(new Board(6, 6, 1), 7);
            player.GenerateDecisionTree();
        }
    }
}