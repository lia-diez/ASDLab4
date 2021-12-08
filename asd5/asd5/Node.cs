using System;

namespace asd5
{
    public class Node
    {
        private Player _player;
        private int Height => _player._board.Height;
        private int Width => _player._board.Width;
        public int[,] State;
        public int Value;
        public int Depth;


        public Node(int[,] newState, Node? parent, Player player, int depth)
        {
            State = newState;
            _player = player;
            Depth = depth;
        }

        public int GetValue(bool isMax)
        {
            int value = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (State[i, j] == 0 && j != Width - 1 && State[i, j + 1] == 0)
                        value++;
                    if (State[i, j] == 0 && i != Height - 1 && State[i + 1, j] == 0)
                        value++;
                }
            }

            if (value == 0) value = Int32.MaxValue;
            else if (value == 1) value = Int32.MinValue;
            else if (value % 2 == 0) value = 1;
            else value = -1;
            return isMax ? value : value * -1;
        }
        
        public static void Copy (int[,]? sourceArray, out int[,] destinationArray)
        {
            destinationArray = new int[sourceArray.GetLength(0), sourceArray.GetLength(1)];
            for (int i = 0; i < destinationArray.GetLength(0); i++)
            {
                for (int j = 0; j < destinationArray.GetLength(1); j++)
                {
                    destinationArray[i, j] = destinationArray[i, j];
                }
            }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    output += State[i, j] + " ";
                }

                output += "\n";
            }
            return output;
        }
    }
}