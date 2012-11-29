using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumbersGame.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string StartingPosition { get; set; }
        public string Moves { get; set; }
        public List<Move> MoveList { get; set; }
        //public virtual int Score { get { return Moves.Count; } }

        public Game()
            : this("", null, null)
        {
        }

        public Game(string name, string startingPosition, string moves)
        {
            Name = name;
            StartingPosition = startingPosition;
            Moves = moves;
            MoveList = ParseMoves(moves);

            /*for (int i = 0; i < moves.Length; i++)
            {
                Moves.Add(new Move(i, moves[i][0], moves[i][1]));
            }*/
                
        }

        public int GetScore()
        {
            return Moves.Split(',').Length / 2;
        }

        public bool IsValid()
        {
            // TODO: Implement
            string[] sp = StartingPosition.Split(',');
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
            }

            return true;
        }

        private List<Move> ParseMoves(string moves)
        {
            string[] mvs = Moves.Split(',');
            List<Move> moveArr = new List<Move>();

            if (mvs.Length % 2 != 0) { return moveArr; }

            for (int i = 0; i < mvs.Length; i += 2)
            {
                try
                {
                    int row = int.Parse(mvs[i]);
                    int col = int.Parse(mvs[i + 1]);
                    moveArr.Add(new Move(i, row, col));
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return moveArr;
        }
    }
}