namespace asd5
{
    public class Board
    {
        public readonly int Height;
        public readonly int Width;
        public Player[] Players;
        public int[,]? CurrentState;

        public Board(int height, int width, int numberOfPLayers)
        {
            Width = width;
            Height = height;
            CurrentState = new int[height,width];
            Players = new Player[numberOfPLayers];
        }


        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    output += CurrentState[i, j] + " ";
                }

                output += "\n";
            }
            return output;
        }
    }
}