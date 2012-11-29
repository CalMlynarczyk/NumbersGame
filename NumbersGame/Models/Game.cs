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
            // TODO: Implement
            /*string[] sp = StartingPosition.Split(',');
            List<int[]> startingPos = new List<int[]>();

            if (sp.Length != 9) { return false; }

            for (int i = 0; i < sp.Length; i += 3)
            {
                try
                {
                    startingPos.Add(new int[] { int.Parse(sp[i]), int.Parse(sp[i + 1]), int.Parse(sp[i + 2]) });
                }
                catch (Exception)
                {
                    return false;
                }
            }

            string[] mvs = Moves.Split(',');
            List<int[]> moveArr = new List<int[]>();

            if (mvs.Length % 2 != 0) { return false; }

            for (int i = 0; i < mvs.Length; i += 2)
            {
                try
                {
                    int row = int.Parse(mvs[i]);
                    int col = int.Parse(mvs[i + 1]);
                    moveArr.Add(new int[] { row, col });
                }
                catch (Exception)
                {
                    return false;
                }
            }*/



            return true;
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
    }
}