using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NumbersGame.Models
{
    public class Game
    {
        private readonly int _boardSize = 3;

        public int GameId { get; set; }
        [RegularExpression("^[0-9A-Za-z_]{1,20}$")]
        public string Name { get; set; }
        public string StartingPosition { get; set; }
        //public string Moves { get; set; }
        //public int[,] StartingPosition { get; set; }
        public virtual List<Move> Moves { get; set; }
        
        public Game()
        {
        }

        public Game(string name, string startingPosition, string moves)
        {
            Name = name;
            StartingPosition = startingPosition;
            Moves = ParseMoves(moves);
            //Moves = moves;                
        }

        public int GetScore()
        {
            return Moves.Count;
            //return Moves.Split(',').Length / 2;
        }

        public bool IsValid()
        {
            int[,] gameBoard = ParseStartingPosition(StartingPosition);
            int zeroRow = -1;
            int zeroCol = -1;

            if (IsGameFinished(gameBoard)) { return false; }

            var rowLowerLimit = gameBoard.GetLowerBound(0);
            var rowUpperLimit = gameBoard.GetUpperBound(0);

            var colLowerLimit = gameBoard.GetLowerBound(1);
            var colUpperLimit = gameBoard.GetUpperBound(1);

            for (int row = rowLowerLimit; row <= rowUpperLimit && zeroRow < 0; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit && zeroRow < 0; col++)
                {
                    if (gameBoard[row, col] == 0)
                    {
                        zeroRow = row;
                        zeroCol = col;
                    }
                }
            }

            if (zeroRow < 0 || zeroCol < 0) { return false; }

            foreach (var move in Moves)
            {
                if (gameBoard[move.Row, move.Column] == 0 ||
                    move.Row < 0 || move.Row > (_boardSize - 1) ||
                    move.Column < 0 || move.Column > (_boardSize - 1)) 
                { return false; }

                if ((move.Row == (zeroRow + 1) && move.Column == zeroCol) ||
                    (move.Row == (zeroRow - 1) && move.Column == zeroCol) ||
                    (move.Row == zeroRow && move.Column == (zeroCol + 1)) ||
                    (move.Row == zeroRow && move.Column == (zeroCol - 1)))
                {
                    gameBoard[zeroRow, zeroCol] = gameBoard[move.Row, move.Column];
                    gameBoard[move.Row, move.Column] = 0;
                    zeroRow = move.Row;
                    zeroCol = move.Column;
                }
                else { return false; }
            }

            return IsGameFinished(gameBoard);
        }

        private bool IsGameFinished(int[,] gameBoard)
        {
            int[,] goalArray = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };

            bool finished = AreArraysEqual(goalArray, gameBoard);

            return finished;
        }

        private int[,] ParseStartingPosition(string startingPosition)
        {
            if (startingPosition == null) { throw new ArgumentException("Invalid starting position string."); }

            string[] sp = startingPosition.Split(',');
            int[,] startingPos = new int[_boardSize,_boardSize];

            if (sp.Length != (_boardSize * _boardSize)) { return startingPos; }

            for (int i = 0; i < sp.Length; i++)
            {
                try
                {
                    startingPos[i / 3, i % 3] = int.Parse(sp[i]);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid starting position string.");
                }
            }

            return startingPos;
        }

        private List<Move> ParseMoves(string moves)
        {
            if (moves == null) { throw new ArgumentException("Invalid moves list."); }

            string[] mvs = moves.Split(',');
            List<Move> moveArr = new List<Move>();

            if (mvs.Length % 2 != 0) { throw new ArgumentException("Invalid moves list."); }

            for (int i = 0; i < mvs.Length; i += 2)
            {
                try
                {
                    int row = int.Parse(mvs[i]);
                    int col = int.Parse(mvs[i + 1]);

                    moveArr.Add(new Move
                    {
                        Sequence = i,
                        Row = row,
                        Column = col
                    });
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid moves list.");
                }
            }

            return moveArr;
        }

        private bool AreArraysEqual(int[,] a, int[,] b)
        {
            var rowLowerLimit = a.GetLowerBound(0);
            var rowUpperLimit = a.GetUpperBound(0);

            var colLowerLimit = a.GetLowerBound(1);
            var colUpperLimit = a.GetUpperBound(1);

            for (int row = rowLowerLimit; row <= rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit; col++)
                {
                    if (a[row, col] != b[row, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}