using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumbersGame.Models;

namespace NumbersGame.ViewModels
{
    public class GameViewModel
    {
        public int[][] StartingPosition { get; set; }
        public int[][] Moves { get; set; }

        public GameViewModel(Game game)
        {
            StartingPosition = ParseStartingPosition(game.StartingPosition);
            Moves = new int[game.Moves.Count][];

            for (int i = 0; i < game.Moves.Count; i++)
            {
                Moves[i] = new int[] { game.Moves.ElementAt(i).Row, game.Moves.ElementAt(i).Column };
            }
        }

        private int[][] ParseStartingPosition(string startingPosition)
        {
            if (startingPosition == null) { throw new ArgumentException("Invalid starting position string."); }

            string[] sp = startingPosition.Split(',');
            int[][] startingPos = new int[3][];

            for (int i = 0; i < sp.Length; i += 3)
            {
                startingPos[i / 3] = new int[] { int.Parse(sp[i]), int.Parse(sp[i + 1]), int.Parse(sp[i + 2]) };
            }

            return startingPos;
        }
    }
}