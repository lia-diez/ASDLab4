using System;
using System.Collections.Generic;

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
            Max(current, alpha, beta);
            DecisionTreeRoot.State = NextStep.State;
            Console.WriteLine(DecisionTreeRoot);

            for (int i = 0; i < 10; i++)
            {
                current = DecisionTreeRoot;
                alpha = Int32.MinValue;
                beta = Int32.MaxValue;
                Min(current, alpha, beta);
                DecisionTreeRoot.State = NextStep.State;
                Console.WriteLine(DecisionTreeRoot);
            
                current = DecisionTreeRoot;
                alpha = Int32.MinValue;
                beta = Int32.MaxValue;
                Max(current, alpha, beta);
                DecisionTreeRoot.State = NextStep.State;
                Console.WriteLine(DecisionTreeRoot);
            }
        }
        
        private int Max(Node current, int alpha, int beta)
        {
            if (current.Depth == MaxDepth)
            {
                return current.GetValue(true);
            }

            bool haveChildren = false;
            int maxValue = Int32.MinValue;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (current.State[i, j] == 0 && j != Width - 1 && current.State[i, j + 1] == 0)
                    {
                        Node.Copy(current.State, out int[,] arr);
                        Node child = new (arr, current, this, current.Depth + 1);
                        
                        child.State[i, j] = 1;
                        child.State[i, j + 1] = 1;
                        
                        bool succeed = Min(child, ref alpha, ref beta, ref maxValue);
                        if (current.Depth == 0 && !haveChildren)
                        {
                            NextStep = child;
                            NextStep.Value = maxValue;
                        }
                        haveChildren = true;
                        if (current.Depth == 0 && maxValue > NextStep.Value) NextStep = child;
                        if (!succeed) return maxValue;
                    }
            
                    if (current.State[i, j] == 0 && i != Height - 1 && current.State[i + 1, j] == 0)
                    {
                        Node.Copy(current.State, out int[,] arr);
                        Node child = new (arr, current, this, current.Depth + 1);

                        child.State[i, j] = 1;
                        child.State[i + 1, j] = 1;

                        bool succeed = Min(child, ref alpha, ref beta, ref maxValue);
                        if (current.Depth == 0 && !haveChildren)
                        {
                            NextStep = child;
                            NextStep.Value = maxValue;
                        }
                        haveChildren = true;
                        if (current.Depth == 0 && maxValue > NextStep.Value) NextStep = child;
                        if (!succeed) return maxValue;
                    }
                }
            }
            
            if (!haveChildren) return Int32.MaxValue;
            return maxValue;
        }
        
        private int Min(Node current, int alpha, int beta)
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

                    if (current.State[i, j] == 0 && j != Width - 1 && current.State[i, j + 1] == 0)
                    {
                        Node.Copy(current.State, out int[,] arr);
                        Node child = new (arr, current, this, current.Depth + 1);
                        child.State[i, j] = -1;
                        child.State[i, j + 1] = -1;

                        bool succeed = Max(child, ref alpha, ref beta, ref minValue);
                        if (current.Depth == 0 && !haveChildren)
                        {
                            NextStep = child;
                            NextStep.Value = minValue;
                        }
                        haveChildren = true;
                        if (current.Depth == 0 && minValue < NextStep.Value) NextStep = child;
                        if (!succeed) return minValue;
                    }

                    if (current.State[i, j] == 0 && i != Height - 1 && current.State[i + 1, j] == 0)
                    {
                        Node.Copy(current.State, out int[,] arr);
                        Node child = new (arr, current, this, current.Depth + 1);
                        child.State[i, j] = -1;
                        child.State[i + 1, j] = -1;
                        
                        bool succeed = Max(child, ref alpha, ref beta, ref minValue);
                        if (current.Depth == 0 && !haveChildren)
                        {
                            NextStep = child;
                            NextStep.Value = minValue;
                        }
                        haveChildren = true;
                        if (current.Depth == 0 && minValue < NextStep.Value) NextStep = child;
                        if (!succeed) return minValue;
                    }
                }
            }
            
            return haveChildren ? minValue : Int32.MinValue;
        }
        
        private bool Min(Node child, ref int alpha, ref int beta, ref int maxValue)
        {
            int newValue = Min(child, alpha, beta);
            if (newValue > maxValue)
            {
                maxValue = newValue;
            }
            if (newValue >= beta) return false;
            if (newValue > alpha) alpha = newValue;
            return true;
        }
        
        private bool Max(Node child, ref int alpha, ref int beta, ref int minValue)
        {
            int newValue = Max(child, alpha, beta);
            if (newValue < minValue)
            {
                minValue = newValue;
            }
            if (newValue <= alpha) return false;
            if (newValue < beta) beta = newValue;
            return true;
        }

    }
}