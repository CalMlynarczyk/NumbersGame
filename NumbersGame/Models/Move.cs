using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumbersGame.Models
{
    public class Move
    {
        public int MoveId { get; set; }
        public int GameId { get; set; }
        public int Sequence { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public virtual Game Game { get; set; }

        public Move(int sequence, int row, int col)
        {
            Sequence = sequence;
            Row = row;
            Column = col;
        }
    }
}