using System;

namespace asd5
{
    public class Player
    {
        public Board _board;
        public Node? DecisionTreeRoot;
        public readonly int MaxDepth;
        public Node NextStep;
        private int Height => _board.Height;
        private int Width => _board.Width;

        public Player(Board board, int maxDepth)
        {
            _board = board;
            MaxDepth = maxDepth;
        }

        public void GenerateDecisionTree()
        {
            DecisionTreeRoot = new Node(_board.CurrentState, null, this, 0);
            
            Node current = DecisionTreeRoot;
            int alpha = Int32.MinValue;
            int beta = Int32.MaxValue;
            Max(current, ref alpha, ref beta);
            DecisionTreeRoot.State = NextStep.State;
            Console.WriteLine(DecisionTreeRoot.State);
            
            current = DecisionTreeRoot;
            alpha = Int32.MinValue;
            beta = Int32.MaxValue;
            Min(current, ref alpha, ref beta);
            DecisionTreeRoot.State = NextStep.State;
            Console.WriteLine(DecisionTreeRoot.State);

        }
        
        private int Max(Node current, ref int alpha, ref int beta)
        {
            if (current.Depth == MaxDepth)
            {
                return current.GetValue(true);
            }

            bool haveChildren = false;
            int maxValue = Int32.MinValue;
            Node step;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Node.Copy(current.State, out int[,] arr);
                    Node child = new (arr, current, this, current.Depth + 1);
                    
                    if (child.State[i, j] == 0 && j != Width - 1 && child.State[i, j + 1] == 0)
                    {
                        haveChildren = true;
                        child.State[i, j] = 1;
                        child.State[i, j + 1] = 1;
                        
                        bool succeed = Min(child, ref alpha, ref beta, ref maxValue);
                        if (!succeed) return maxValue;
                        
                        child.State[i, j] = 0;
                        child.State[i, j + 1] = 0;
                    }
                    
                    Node.Copy(current.State, out arr);
                    child = new (arr, current, this, current.Depth + 1);    
                    
                    if (child.State[i, j] == 0 && i != Height - 1 && child.State[i + 1, j] == 0)
                    {
                        haveChildren = true;
                        child.State[i, j] = 1;
                        child.State[i + 1, j] = 1;

                        bool succeed = Min(child, ref alpha, ref beta, ref maxValue);
                        if (!succeed) return maxValue;
                        
                        child.State[i, j] = 0;
                        child.State[i + 1, j] = 0;
                    }
                }
            }
            
            if (!haveChildren) return Int32.MaxValue;
            return maxValue;
        }
        
        private int Min(Node current, ref int alpha, ref int beta)
        {
            if (current.Depth == MaxDepth)
            {
                return current.GetValue(false);
            }
            
            bool haveChildren = false;
            int minValue = Int32.MaxValue;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Node.Copy(current.State, out int[,] arr);
                    Node child = new (arr, current, this, current.Depth + 1);
                    
                    if (child.State[i, j] == 0 && j != Width - 1 && child.State[i, j + 1] == 0)
                    {
                        haveChildren = true;
                        child.State[i, j] = -1;
                        child.State[i, j + 1] = -1;
                        
                        bool succeed = Max(child, ref alpha, ref beta, ref minValue);
                        if (!succeed) return minValue;
                        
                        child.State[i, j] = 0;
                        child.State[i, j + 1] = 0;
                    }
                    
                    Node.Copy(current.State, out arr);
                     child = new (arr, current, this, current.Depth + 1);    
                     
                     if (child.State[i, j] == 0 && i != Height - 1 && child.State[i + 1, j] == 0)
                    {
                        haveChildren = true;
                        child.State[i, j] = -1;
                        child.State[i + 1, j] = -1;
                        
                        bool succeed = Max(child, ref alpha, ref beta, ref minValue);
                        if (!succeed) return minValue;
                        
                        child.State[i, j] = 0;
                        child.State[i + 1, j] = 0;
                    }
                }
            }
            
            return haveChildren ? minValue : Int32.MinValue;
        }
        
        private bool Min(Node child, ref int alpha, ref int beta, ref int maxValue)
        {
            int newValue = Min(child, ref alpha, ref beta);
            if (newValue >= maxValue)
            {
                maxValue = newValue;
                if (child.Depth == 2) NextStep = child;
            }
            if (newValue >= beta) return false;
            if (newValue > alpha) alpha = newValue;
            return true;
        }
        
        private bool Max(Node child, ref int alpha, ref int beta, ref int minValue)
        {
            int newValue = Max(child, ref alpha, ref beta);
            if (newValue <= minValue)
            {
                minValue = newValue;
                if (child.Depth == 2) NextStep = child;
            }
            if (newValue <= alpha) return false;
            if (newValue < beta) beta = newValue;
            return true;
        }
    }
}